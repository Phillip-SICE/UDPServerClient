using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    public class ClientLoginSignUpEvent
    {
        public ClientLoginSignUpEvent(Type type, string username, string password)
        {
            this.EventType = type;
            this.Username = username;
            this.Password = password;
        }

        public readonly string Username;
        public readonly string Password;
        public readonly Type EventType;

        public enum Type
        {
            Login,
            SignUp
        }
    }


}
