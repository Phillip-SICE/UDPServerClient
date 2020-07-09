using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    interface IServerInterface
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

        string ReceivedData
        {
            get;
            set;
        }

        void Connect();

        void GetData();

        void DisplayData();
        
    }
}
