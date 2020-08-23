using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI.ViewModels
{
    class LoginViewModel : Screen
    {
        private IEventAggregator _eventAggregator;
        private string _username;
        private string _password;

        public LoginViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                NotifyOfPropertyChange(nameof(Username));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                NotifyOfPropertyChange(nameof(Password));
            }
        }

        public bool CanLogin(string username, string password)
        {
            if (username is null || username == "") return false;
            if (password is null || password == "") return false;
            return true;
        }

        public void Login(string username, string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _eventAggregator.PublishOnBackgroundThread(new ClientLoginEvent(Username, passwordHash));
        }
    }
}
