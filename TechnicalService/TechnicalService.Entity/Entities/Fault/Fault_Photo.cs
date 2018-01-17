using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Fault
{
    [Table("Fault_Photos")]
    public class Fault_Photo : BaseEntity<int>
    {
        [Required]
        public string URL { get; set; }
        public int FaultID { get; set; }
        [ForeignKey("FaultID")]
        public Fault Fault { get; set; }
    }
}
