using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sice.PoC.UDPServer
{
    [Table("Login")]
    public class Login
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginID { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int ControllerID { get; set; }

        public virtual Controller Controller { get; set; }
    }
}
