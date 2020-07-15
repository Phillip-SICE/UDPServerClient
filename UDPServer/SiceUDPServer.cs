using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class SiceUDPServer : IServerInterface
    {
        private UdpClient client;

        private int currentPort;

        public SiceUDPServer()
        {
            this.ConnectionStatus = false;
            this.MessageReceived = new ObservableCollection<string>();
            this.MessageReceivedNumber = 0;
            this.currentPort = -1;
        }

        public IPAddress ConnectionIP { get; set; }

        public int ConnectionPort { get; set; }

        public string ReceivedData { get; set; }

        public bool ConnectionStatus { get; set; }

        public ObservableCollection<string> MessageReceived { get; set; }

        public int MessageReceivedNumber { get; set; }

        public int DataAvailable => client.Available;
        
        public void Connect()
        {
            if(!ConnectionStatus)
            {
                client = new UdpClient(ConnectionPort);
                currentPort = ConnectionPort;
                this.ConnectionStatus = true;
            }
        }

        public void Disconnect()
        {
            if(ConnectionStatus)
            {
                client.Close();
                this.ConnectionStatus = false;
            }
        }

        public async Task GetData()
        {
            var remoteIpEndPoint = new IPEndPoint(ConnectionIP, ConnectionPort);
            var data = await client.ReceiveAsync();
            ReceivedData = Encoding.ASCII.GetString(data.Buffer);
            this.MessageReceivedNumber++;
            this.MessageReceived.Add($"{this.MessageReceivedNumber.ToString()}. {ReceivedData}");
        }

        public void ClearMessage()
        {
            this.MessageReceived.Clear();
            this.MessageReceivedNumber = 0;
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
