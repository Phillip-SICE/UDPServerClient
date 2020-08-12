namespace UDPCommGUI
{
    class ClientStatusChangedEvent
    {
        public ClientStatusChangedEvent(bool status)
        {
            this.Status = status;
        }

        public bool Status { get; private set; }
    }
}
