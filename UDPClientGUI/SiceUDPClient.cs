using Caliburn.Micro;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPCommGUI;

namespace Sice.PoC.UDPCommGUI
{
    class SiceUDPClient : IClientInterface, IHandle<ClientCommandEvent>
    {

        private UdpClient client;
        private IEventAggregator _eventAggregator;
    
        public SiceUDPClient(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);
            this.ConnectionStatus = false;
        }

        public IPAddress ConnectionIP { get; set; }

        public int ConnectionPort { get; set; }

        public string InputMessage{ get; set; }

        public bool ConnectionStatus { get; set; }
                
        public void Connect()
        {
            this.client = new UdpClient();
            client.Connect(ConnectionIP, ConnectionPort);
            this.ConnectionStatus = true;
            this._eventAggregator.PublishOnUIThread(new ClientStatusChangedEvent(ConnectionStatus));

        }

        public void Disconnect()
        {
            if(ConnectionStatus)
            {
                client.Close();
                this.ConnectionStatus = false;
                this._eventAggregator.PublishOnUIThread(new ClientStatusChangedEvent(ConnectionStatus));
            }
        }

        public void SendMessage()
        {
            if(ConnectionStatus)
            {
                var message = Encoding.ASCII.GetBytes(InputMessage);
                client.Send(message, message.Length);
            }            
        }

        public void Dispose()
        {
            client.Dispose();
        }

        public void Handle(ClientCommandEvent Command)
        {
            ConnectionIP = Command.ConnectionIP;
            ConnectionPort = Command.ConnectionPort;
            if (Command.ClientCommand == ClientCommandEvent.Command.Connect) {
                Connect();
                return;
            }
            if (Command.ClientCommand == ClientCommandEvent.Command.Disconnect) {
                Disconnect();
                return;
            }
            if (Command.ClientCommand == ClientCommandEvent.Command.SendMessage)
            {
                InputMessage = Command.Message;
                SendMessage();
                return;
            }
        }
    }
}
