using System;
using System.Net;

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
