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
            set => Set<string>(ref _username, value, nameof(Username));
        }

        public string Password
        {
            get => _password;
            set => Set<string>(ref _password, value, nameof(Password));
        }

        public bool CanLogin(string username, string password)
        {
            if(string.IsNullOrEmpty(username.Trim()) || string.IsNullOrEmpty(password.Trim()))
            {
                return false;
            }
            return true;
        }

        public void Login(string username, string password)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientLoginSignUpEvent(ClientLoginSignUpEvent.Type.Login, Username, Password));
        }

        public bool CanSignUp(string username, string password)
        {
            return CanLogin(username, password);
        }

        public void SignUp(string username, string password)
        {
            _eventAggregator.PublishOnBackgroundThread(new ClientLoginSignUpEvent(ClientLoginSignUpEvent.Type.SignUp, Username, Password));
        }

    }
}
