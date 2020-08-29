using Caliburn.Micro;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UDPCommGUI;
using Newtonsoft.Json;

namespace Sice.PoC.UDPCommGUI
{
    class SiceUDPClient : IClientInterface, IHandle<ClientCommandEvent>, IHandle<ClientLoginEvent>
    {
        private Dictionary<ClientCommandEvent.Command, System.Action> handler = new Dictionary<ClientCommandEvent.Command, System.Action>();
        private UdpClient client;
        private IEventAggregator _eventAggregator;


        public SiceUDPClient(IEventAggregator eventAggregator)
        {
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);
            this.ConnectionStatus = false;

            
            handler.Add(ClientCommandEvent.Command.Connect, Connect);
            handler.Add(ClientCommandEvent.Command.Disconnect, Disconnect);
            handler.Add(ClientCommandEvent.Command.SendMessage, SendMessage);
            
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

            if (!handler.ContainsKey(Command.ClientCommand)) return;
            InputMessage = Command.Message;
            ConnectionIP = Command.ConnectionIP;
            ConnectionPort = Command.ConnectionPort;
            handler[Command.ClientCommand]();
        }

        public void Handle(ClientLoginEvent message)
        {
            if (!ConnectionStatus) return;
            var loginMessage = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(message));
            client.Send(loginMessage, loginMessage.Length);
        }
    }
}
