﻿using Caliburn.Micro;
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
            set => Set<string>(ref _iP, value, nameof(IP));
        }

        public string Port
        {
            get => _port;
            set => Set<string>(ref _port, value, nameof(Port));
        }

        public string ListeningStatus
        {
            get => _listeningStatus;
            set => Set<string>(ref _listeningStatus, value, nameof(ListeningStatus));
        }

        public ObservableCollection<string> ReceivedMessages { get; set; }
        
        public bool CanConnect(string iP, string port)
        {
            var validIP = IPAddress.TryParse(iP, out IPAddress parseIP);
            var validPort = int.TryParse(port, out int parsePort);
            return validIP && validPort;
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
            this.ListeningStatus = status.Status ? "Listening" : "Stopped";
        }

        
    }

}
