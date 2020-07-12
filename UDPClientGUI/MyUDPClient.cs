using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    class MyUDPClient : IClientInterface
    {

        private UdpClient client;
    
        public MyUDPClient()
        {
            this.ConnectionStatus = false;
        }

        public string ConnectionIP { get; set; }

        public int ConnectionPort { get; set; }

        public string InputMessage{ get; set; }

        public bool ConnectionStatus { get; set; }
                
        public void Connect()
        {
            this.client = new UdpClient();
            client.Connect(ConnectionIP, ConnectionPort);
            this.ConnectionStatus = true;
        }

        public void Disconnect()
        {
            if(ConnectionStatus)
            {
                client.Close();
                this.ConnectionStatus = false;
            }
        }

        public void SendMessage()
        {
            if(ConnectionStatus)
            {
                byte[] message = Encoding.ASCII.GetBytes(InputMessage);
                client.Send(message, message.Length);
            }            
        }
    }
}
