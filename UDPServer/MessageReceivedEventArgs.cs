using System;

namespace UDPServer
{
    public class MessageReceivedEventArgs: EventArgs
    {
        public MessageReceivedEventArgs(string args)
        {
            ReceivedMessage = args;
        }
        public string ReceivedMessage { get; set; }
    }
}
