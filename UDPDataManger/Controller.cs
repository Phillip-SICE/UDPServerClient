using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sice.PoC.UDPServer
{
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
