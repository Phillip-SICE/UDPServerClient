using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UDPCommGUI
{
    class SiceUDPClient : IClientInterface
    {

        private UdpClient client;
    
        public SiceUDPClient()
        {
            this.ConnectionStatus = false;
        }

        public IPAddress ConnectionIP { get; set; }

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
                var message = Encoding.ASCII.GetBytes(InputMessage);
                client.Send(message, message.Length);
            }            
        }

        public void Dispose()
        {
            client.Dispose();
        }
    }
}
