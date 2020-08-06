using System.Data.Entity;

namespace Sice.PoC.UDPServer
{
    public class ServerContext : DbContext
    {
        public ServerContext(): base("UDPServerConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ServerContext>());
        }
        public DbSet<ReceivedMessage> ReceivedMessages { get; set; }
    }
}
