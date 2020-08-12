namespace UDPServer
{
    public class ServerStatusChangedEvent
    {
        public ServerStatusChangedEvent(bool status)
        {
            this.Status = status;
        }
        public bool Status { get; private set; }
    }
}
