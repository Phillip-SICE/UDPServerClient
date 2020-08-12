using Caliburn.Micro;
using Sice.PoC.UDPCommGUI;
using System.Configuration;
using System.Net;

namespace UDPCommGUI.ViewModels
{
    class ShellViewModel : Screen, IHandle<ClientStatusChangedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private string _iP;
        private string _port;
        private string _message;
        private string _connectionStatus;

        public ShellViewModel(IEventAggregator eventAggregator, SiceUDPClient siceUDPClient)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            IP = ConfigurationManager.AppSettings["IPAddress"];
            Port = ConfigurationManager.AppSettings["PortNumber"];
            ConnectionStatus = "Disconnected";
        }

        public string IP
        {
            get => _iP;
            set
            {
                _iP = value;
                NotifyOfPropertyChange(nameof(IP));
            }
        }

        public string Port
        {
            get => _port;
            set
            {
                _port = value;
                NotifyOfPropertyChange(nameof(Port));
            }
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                NotifyOfPropertyChange(nameof(ConnectionStatus));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                NotifyOfPropertyChange(nameof(Message));
            }
        }

        public bool CanConnect(string iP, string port)
        {
            if (!IPAddress.TryParse(iP, out IPAddress parseIP)) return false;
            if (!int.TryParse(port, out int parsePort)) return false;
            return true;
        }

        public void Connect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientCommandEvent(ClientCommandEvent.Command.Connect, IP, Port));
        }

        public void Disconnect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientCommandEvent(ClientCommandEvent.Command.Disconnect, IP, Port));
        }

        public bool CanSend(string message)
        {
            if (ConnectionStatus == "Connected") return !(message is null || message == "");
            return false;
        }

        public void Send(string message)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientCommandEvent(ClientCommandEvent.Command.SendMessage, IP, Port, Message));
        }

        public void Handle(ClientStatusChangedEvent Status)
        {
            ConnectionStatus = Status.Status ? "Connected" : "Disconnected";
        }
    }
}
