using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    public class ClientLoginEvent
    {
        public ClientLoginEvent(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public readonly string Username;
        public readonly string Password;
    }
}
