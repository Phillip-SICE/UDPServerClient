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
<<<<<<< Updated upstream
=======
        public DbSet<Controller> Controllers { get; set; }
        public DbSet<Login> Login { get; set; }

        
>>>>>>> Stashed changes
    }
}
