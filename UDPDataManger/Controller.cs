using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sice.PoC.UDPServer
{
    //public class Controller
    //{
    //    [Key]
    //    public int ControllerID;
    //    public string ControllerInfo;

    //    public virtual ICollection<ReceivedMessage> ReceivedMessages { get; set; }
    //}

    [Table("Controller")]
    public  class Controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Controller()
        {
            Logins = new HashSet<Login>();
            ReceivedMessages = new HashSet<ReceivedMessage>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ControllerID { get; set; }

        [StringLength(50)]
        public string ControllerInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Login> Logins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ReceivedMessage> ReceivedMessages { get; set; }
    }
}
