namespace UDPCommGUI
{
    public class ClientLoginEvent
    {
        public ClientLoginEvent(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }

        public readonly string Username;
        public readonly string Password;
    }
}
