using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    class MyUDPClient : UdpClient, IClientInterface
    {

        private string connectionIP;
        private int connectionPort;
        private string inputMessage;


        public string ConnectionIP
        {
            get { return connectionIP; }
            set { connectionIP = value; }
        }

        public int ConnectionPort
        {
            get { return connectionPort; }
            set { connectionPort = value; }
        }

        public string InputMessage
        {
            get { return inputMessage; }
            set { inputMessage = value; }
        }

        public void Connect()
        {
            this.Connect(ConnectionIP, ConnectionPort);
        }

        public void DisplayInfo()
        {
            //
        }

        public void SendMessage()
        {
            byte[] message = Encoding.ASCII.GetBytes(InputMessage);
            this.Send(message, message.Length);
        }
    }
}
