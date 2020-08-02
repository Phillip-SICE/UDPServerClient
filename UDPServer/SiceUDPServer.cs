using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UDPServer;

namespace Sice.PoC.UDPServer
{
    class SiceUDPServer : IServerInterface
    {
        private UdpClient client;
        CancellationTokenSource source;
        CancellationToken token;
        private int currentPort;
        public IPAddress ConnectionIP { get; set; }
        public int ConnectionPort { get; set; }
        public string ReceivedData { get; set; }
        public bool ConnectionStatus { get; set; }
        public ReceivedMessage ReceivedMessage { get; set; }
        public int DataAvailable => client.Available;

        public SiceUDPServer()
        {
            this.ConnectionStatus = false;
            this.currentPort = -1;
        }

        public void Connect()
        {
            if(!ConnectionStatus)
            {
                client = new UdpClient(ConnectionPort);
                currentPort = ConnectionPort;
                this.ConnectionStatus = true;
                source = new CancellationTokenSource();
                token = source.Token;
                Task.Run(() => Listen(), token);
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
            }
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceivedEventHandler;

        protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
        {
            MessageReceivedEventHandler?.Invoke(this, e);
        }

        public async Task GetData()
        {
            var remoteIpEndPoint = new IPEndPoint(ConnectionIP, ConnectionPort);
            var data = await client.ReceiveAsync();
            ReceivedData = Encoding.ASCII.GetString(data.Buffer);
            ReceivedMessage = new ReceivedMessage(DateTime.Now.ToString(), data.RemoteEndPoint.Address.ToString(), ReceivedData);
            OnMessageReceived(new MessageReceivedEventArgs(ReceivedMessage));
            using (var db = new ServerContext())
            {
                db.ReceivedMessages.Add(ReceivedMessage);
                db.SaveChanges();
            }
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
