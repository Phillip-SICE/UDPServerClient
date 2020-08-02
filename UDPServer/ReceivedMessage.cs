using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Sice.PoC.UDPServer
{
    public class ReceivedMessage
    {
        public ReceivedMessage(string ReceivedTime, string Source, string Message)
        {
            this.ReceivedTime = ReceivedTime;
            this.Source = Source;
            this.Message = Message;
        }

        public int MessageID { get; set; }
        public string ReceivedTime { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }        
    }
}
