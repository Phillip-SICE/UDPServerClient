﻿using Caliburn.Micro;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer;

namespace Sice.PoC.UDPServer
{
    public class SiceUDPServer : IServerInterface, IHandle<ServerCommandEvent>
    {
        private IEventAggregator eventAggregator;
        private UdpClient client;
        private CancellationTokenSource source;
        private CancellationToken token;
        public IPAddress ConnectionIP { get; set; }
        public int ConnectionPort { get; set; }
        public string ReceivedData { get; set; }
        public bool ConnectionStatus { get; set; }
        public ReceivedMessage ReceivedMessage { get; set; }
        public int DataAvailable => client.Available;

        public SiceUDPServer(IEventAggregator eventAggregator)
        {
            this.ConnectionStatus = false;
            this.eventAggregator = eventAggregator;
            eventAggregator.Subscribe(this);
        }

        public void Connect()
        {
            if(!ConnectionStatus)
            {
                client = new UdpClient(ConnectionPort);
                source = new CancellationTokenSource();
                token = source.Token;
                Task.Run(() => Listen(), token);
                this.ConnectionStatus = true;
                eventAggregator.PublishOnUIThread(new ServerStatusChangedEvent(true));
            }
        }

        private async Task Listen()
        {
            while (true)
            {
                if (DataAvailable != 0)
                {
                    await GetData();
                }
            }
        }

        public void Disconnect()
        {
            if(ConnectionStatus)
            {
                client.Close();
                this.ConnectionStatus = false;
                source.Cancel();
                eventAggregator.PublishOnUIThread(new ServerStatusChangedEvent(false));
            }
        }

        public async Task GetData()
        {
            var remoteIpEndPoint = new IPEndPoint(ConnectionIP, ConnectionPort);
            var data = await client.ReceiveAsync();
            ReceivedData = Encoding.ASCII.GetString(data.Buffer);
            ReceivedMessage = new ReceivedMessage(DateTime.Now.ToString(), data.RemoteEndPoint.Address.ToString(), ReceivedData);
            this.eventAggregator.PublishOnUIThread(ReceivedMessage);
            using (var db = new ServerContext())
            {
                db.ReceivedMessages.Add(ReceivedMessage);
                await db.SaveChangesAsync();
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Handle(ServerCommandEvent command)
        {
            if(command.Command)
            {
                ConnectionIP = command.ConnectionIP;
                ConnectionPort = command.ConnectionPort;
                this.Connect();
            }
            else
            {
                this.Disconnect();
            }
        }
    }
}
