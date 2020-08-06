using System;

namespace Sice.PoC.UDPServer
{
    public class MessageReceivedEventArgs: EventArgs
    {
        public MessageReceivedEventArgs(ReceivedMessage args)
        {
            ReceivedMessage = args;
        }
        public ReceivedMessage ReceivedMessage { get; set; }
    }
}
