using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sice.PoC.UDPServer
{
    public class Login
    {

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LoginID { get; set; }
        //public string Username { get; set; }
        //public string PasswordHash { get; set; }
        //[ForeignKey("ControllerID")]
        //public virtual Controller Controller { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public int ControllerID { get; set; }

        public virtual Controller Controller { get; set; }
    }
}
