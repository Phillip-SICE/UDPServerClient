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
        public Command ClientCommand { get; private set; }
        public IPAddress ConnectionIP { get; private set; }
        public int ConnectionPort { get; private set; }
        public string Message { get; private set; }

        public enum Command
        {
            Connect,
            Disconnect,
            SendMessage
        };
    }
}
