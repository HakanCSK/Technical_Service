using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.Enums;

namespace TechnicalService.Entity.Entities.Fault
{
    [Table("Fault_Statuses")]
    public class Fault_Status : BaseEntity<int>
    {
        public int FaultID { get; set; }
        [Required]
        public FaultStatus Status { get; set; }
        public string Description { get; set; }

        [ForeignKey("FaultID")]
        public Fault Fault { get; set; }

    }
}
