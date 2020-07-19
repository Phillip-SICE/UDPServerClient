using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sice.PoC.UDPCommGUI
{
    interface IClientInterface : IDisposable
    {
        IPAddress ConnectionIP { get; set; }

        int ConnectionPort { get; set; }

        string InputMessage { get; set; }

        bool ConnectionStatus { get; }

        void Connect();

        void Disconnect();

        void SendMessage();
    }
}
