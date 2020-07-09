using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPServer
{
    class MyUDPServer : IServerInterface
    {
        private UdpClient client;
        private string connectionIP;
        private int connectionPort;
        private string receivedData;

        public MyUDPServer()
        {

        }



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

        public string ReceivedData
        {
            get { return receivedData; }
            set { receivedData = value; }
        }

        public void Connect()
        {
            client = new UdpClient(ConnectionPort);
        }

        public void GetData()
        {
            // IPEndPoint remoteIpEndPoint = new IPEndPoint(IPtoLong(ConnectionIP), ConnectionPort);
            IPEndPoint remoteIpEndPoint = new IPEndPoint(IPAddress.Any, ConnectionPort);
            byte[] data = client.Receive(ref remoteIpEndPoint);
            ReceivedData = Encoding.ASCII.GetString(data);
        }

        public void DisplayData()
        {
            //
        }

        long IPtoLong(string IP)
        {
            string[] IPs = IP.Split('.');
            long result = 0;
            int i = 24;
            foreach(string s in IPs)
            {
                result += long.Parse(s) * 2 ^ i;
                i -= 8;
            }

            return result;
        }

    }
}
