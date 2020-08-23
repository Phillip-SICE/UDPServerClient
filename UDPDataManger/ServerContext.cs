using System.Data.Entity;

namespace Sice.PoC.UDPServer
{
    public class ServerContext : DbContext
    {
        public ServerContext(): base("UDPServerConnectionString")
        {
        }
        public DbSet<ReceivedMessage> ReceivedMessages { get; set; }
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<Login> Logins { get; set; }

        
    }
}
