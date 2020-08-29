namespace UDPServer
{
    public class ServerStatusChangedEvent
    {
        public ServerStatusChangedEvent(ServerStatus status, string ControllerInfo = "")
        {
            this.Status = status;
            this.ControllerInfo = ControllerInfo;
        }
        public readonly ServerStatus Status;
        public readonly string ControllerInfo;
    }
}
