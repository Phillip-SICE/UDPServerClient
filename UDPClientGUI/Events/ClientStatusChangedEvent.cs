namespace UDPCommGUI
{
    class ClientStatusChangedEvent
    {
        public ClientStatusChangedEvent(bool status)
        {
            this.Status = status;
        }

        public readonly bool Status;
    }
}
