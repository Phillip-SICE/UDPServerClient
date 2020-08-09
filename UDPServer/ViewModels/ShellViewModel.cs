using Caliburn.Micro;
using Sice.PoC.UDPServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net;
using System.Windows.Media;

namespace UDPServer.ViewModels
{
    public class ShellViewModel : Screen, IHandle<ReceivedMessage>, IHandle<SiceUDPServer.ServerStatus>
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly SiceUDPServer server;
        public ShellViewModel(IEventAggregator eventAggregator, SiceUDPServer siceUDPServer)
        {
            this.IP = ConfigurationManager.AppSettings["IPAddress"];
            this.Port = ConfigurationManager.AppSettings["PortNumber"];
            this.server = siceUDPServer;
            this.ReceivedMessages = new ObservableCollection<string>();
            this._eventAggregator = eventAggregator;
            this._eventAggregator.Subscribe(this);
        }

        private string _iP;
        private string _port;
        private string _listeningStatus;
        private SolidColorBrush _statusTextColour;

        public string IP
        {
            get { return _iP; }
            set {
                _iP = value;
                NotifyOfPropertyChange(() => IP);
            }
        }

        public string Port
        {
            get { return _port; }
            set
            {
                _port = value;
                NotifyOfPropertyChange(() => Port);
            }
        }

        public string ListeningStatus
        {
            get { return _listeningStatus; }
            set
            {
                _listeningStatus = value;
                NotifyOfPropertyChange(() => ListeningStatus);
            }
        }

        public SolidColorBrush StatusTextColour
        {
            get { return this._statusTextColour; }
            set
            {
                this._statusTextColour = value;
                NotifyOfPropertyChange(() => StatusTextColour);
            }
        }

        public ObservableCollection<string> ReceivedMessages { get; set; }
        
        public bool CanConnect(string iP, string port)
        {
            if (!IPAddress.TryParse(iP, out IPAddress parseIP))
            {
                return false;
            }
            if (!int.TryParse(port, out int parsePort))
            {
                return false;
            }
            return true;
        }

        public void Connect(string iP, string port)
        {
            this.StatusTextColour = new SolidColorBrush(Colors.Green);
            _eventAggregator.PublishOnBackgroundThread(new SiceUDPServer.ServerCommand(true, IP, Port));
        }

        public void Disconnect(string iP, string port)
        {
            this.StatusTextColour = new SolidColorBrush(Colors.Red);
            _eventAggregator.PublishOnBackgroundThread(new SiceUDPServer.ServerCommand(false, IP, Port));
        }

        public void Clear()
        {
            this.ReceivedMessages.Clear();
        }

        public void Handle(ReceivedMessage message)
        {
            this.ReceivedMessages.Add(message.Message);
        }

        public void Handle(SiceUDPServer.ServerStatus status)
        {
            if(status.Status)
            {
                this.ListeningStatus = "Listening";
            }
            else
            {
                this.ListeningStatus = "Stopped";
            }
        }

        
    }

}
