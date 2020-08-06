using System;
using System.Net;

namespace Sice.PoC.UDPServer
{
    interface IServerInterface : IDisposable
    {
        IPAddress ConnectionIP { get; set; }

        int ConnectionPort { get; set; }

        string ReceivedData { get; set; }

        void Connect();

        void Disconnect();
    }
}
