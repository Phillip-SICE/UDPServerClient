using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    interface IClientInterface
    {
        string ConnectionIP
        {
            get;
            set;
        }

        int ConnectionPort
        {
            get;
            set;
        }

        string InputMessage
        {
            get;
            set;
        }

        void Connect();

        void SendMessage();

        void DisplayInfo();

    }
}
