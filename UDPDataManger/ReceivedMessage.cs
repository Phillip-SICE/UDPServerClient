using System.ComponentModel.DataAnnotations;

namespace Sice.PoC.UDPServer
{
    public class ReceivedMessage
    {
        public ReceivedMessage(string ReceivedTime, string Source, string Message, int ControllerID)
        {
            this.ReceivedTime = ReceivedTime;
            this.Source = Source;
            this.Message = Message;
            this.ControllerID = ControllerID;
        }

        [Key]
        public int MessageId { get; set; }
        public string ReceivedTime { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }        
        public int ControllerID { get; set; }
    }
}
