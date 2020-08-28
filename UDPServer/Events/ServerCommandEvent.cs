using System.Net;

namespace UDPServer
{
    public class ServerCommandEvent
    {
        public ServerCommandEvent(bool command, string IP, string port)
        {
            this.Command = command;
            if (IPAddress.TryParse(IP, out IPAddress parseIP))
            {
                ConnectionIP = parseIP;
            }
            if (int.TryParse(port, out int parsePort))
            {
                ConnectionPort = parsePort;
            }
        }
        public bool Command { get; private set; }
        public IPAddress ConnectionIP { get; private set; }
        public int ConnectionPort { get; private set; }
    }
}
