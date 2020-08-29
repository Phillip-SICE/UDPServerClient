using Caliburn.Micro;
using Sice.PoC.UDPCommGUI;
using System.Configuration;
using System.Net;

namespace UDPCommGUI.ViewModels
{
    class ShellViewModel : Screen, IHandle<ClientStatusChangedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly LoginViewModel _loginViewModel;
        private string _iP;
        private string _port;
        private string _message;
        private string _connectionStatus;

        public ShellViewModel(IEventAggregator eventAggregator, SiceUDPClient siceUDPClient, LoginViewModel loginViewModel)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
            _loginViewModel = loginViewModel;
            IP = ConfigurationManager.AppSettings["IPAddress"];
            Port = ConfigurationManager.AppSettings["PortNumber"];
            ConnectionStatus = "Disconnected";
        }

        public string IP
        {
            get => _iP;
            set => Set<string>(ref _iP, value, nameof(IP));
        }

        public string Port
        {
            get => _port;
            set => Set<string>(ref _port, value, nameof(Port));
        }

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set => Set<string>(ref _connectionStatus, value, nameof(ConnectionStatus));
        }

        public string Message
        {
            get => _message;
            set => Set<string>(ref _message, value, nameof(Message));
        }

        public bool CanConnect(string iP, string port)
        {
            var validIP = IPAddress.TryParse(iP, out IPAddress parseIP);
            var validPort = int.TryParse(port, out int parsePort);
            return validIP && validPort;
        }

        public void Connect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientCommandEvent(ClientCommandEvent.Command.Connect, IP, Port));
        }

        public void Disconnect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientCommandEvent(ClientCommandEvent.Command.Disconnect, IP, Port));
        }

        public void Login()
        {
            IWindowManager manager = new WindowManager();
            manager.ShowDialog(_loginViewModel, null, null);
        }

        public bool CanSend(string message)
        {
            var connected = (ConnectionStatus == "Connected");
            var emptyMessage = string.IsNullOrEmpty(message);
            return (connected && !emptyMessage);
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
