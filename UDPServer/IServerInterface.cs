using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
