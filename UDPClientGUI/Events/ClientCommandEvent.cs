using System.Net;

namespace UDPCommGUI
{
    class ClientCommandEvent
    {
        public ClientCommandEvent(Command command, string IP, string port, string message = null)
        {
            this.ClientCommand = command;
            this.Message = message;
            if (IPAddress.TryParse(IP, out IPAddress parseIP))
            {
                ConnectionIP = parseIP;
            }
            if (int.TryParse(port, out int parsePort))
            {
                ConnectionPort = parsePort;
            }
        }
        public readonly Command ClientCommand;
        public readonly IPAddress ConnectionIP;
        public readonly int ConnectionPort;
        public readonly string Message;

        public enum Command
        {
            Connect,
            Disconnect,
            SendMessage
        };
    }
}
