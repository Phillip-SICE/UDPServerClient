using Caliburn.Micro;
using Sice.PoC.UDPServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net;

namespace UDPServer.ViewModels
{
    public class ShellViewModel : Screen, IHandle<ReceivedMessage>, IHandle<ServerStatusChangedEvent>
    {
        private readonly IEventAggregator _eventAggregator;
        private string _iP;
        private string _port;
        private string _listeningStatus;
        private string _controllerInfo;

        public ShellViewModel(IEventAggregator eventAggregator, SiceUDPServer siceUDPServer)
        {
            this.IP = ConfigurationManager.AppSettings["IPAddress"];
            this.Port = ConfigurationManager.AppSettings["PortNumber"];
            this.ListeningStatus = "Stopped";
            this.ReceivedMessages = new ObservableCollection<string>();
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);
        }
        
        public string IP
        {
            get => _iP;
            set {
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

        public string ListeningStatus
        {
            get => _listeningStatus;
            set
            {
                _listeningStatus = value;
                NotifyOfPropertyChange(nameof(ListeningStatus));
            }
        }

        public string ControllerInfo
        {
            get => _controllerInfo;
            set
            {
                _controllerInfo = value;
                NotifyOfPropertyChange(nameof(ControllerInfo));
            }
        }

        public ObservableCollection<string> ReceivedMessages { get; set; }
        
        public bool CanConnect(string iP, string port)
        {
            if (!IPAddress.TryParse(iP, out IPAddress parseIP)) return false;
            if (!int.TryParse(port, out int parsePort)) return false;
            return true;
        }

        public void Connect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ServerCommandEvent(true, IP, Port));
        }

        public void Disconnect(string iP, string port)
        {
            _eventAggregator.PublishOnBackgroundThread(new ServerCommandEvent(false, IP, Port));
        }

        public void Clear()
        {
            this.ReceivedMessages.Clear();
        }

        public void Handle(ReceivedMessage message)
        {
            this.ReceivedMessages.Add(message.Message);
        }

        public void Handle(ServerStatusChangedEvent status)
        {
            //this.ListeningStatus = status.Status ? "Listening" : "Stopped";
            this.ListeningStatus = status.Status.ToString();
            if(status.Status == ServerStatus.Listening)
            {
                ControllerInfo = $"on {status.ControllerInfo}";
            }
            else
            {
                ControllerInfo = "";
            }
            
        }

        
    }

}
