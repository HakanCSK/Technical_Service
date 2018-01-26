using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.Entities.Product;
using TechnicalService.Entity.IdentityModels;

namespace TechnicalService.Entity.Entities.Fault
{
    [Table("Faults")]
    public class Fault : BaseEntity<int>
    {
        [Required]
        public int ModelID { get; set; }
        [Required]
        public string Description { get; set; }
        public string UserID { get; set; }
        public string OperatorID { get; set; }
        public string TechnicianID { get; set; }
        [Required]
        public string Title { get; set; }
        public string Report { get; set; }
        public string InvoiceCode { get; set; }
        [Required]
        public DateTime AppointmentDate { get; set; } = DateTime.Now;
        
        public string Address { get; set; }

        public List<Fault_Status> Statuses { get; set; } = new List<Fault_Status>();
        public List<Fault_Photo> Photos { get; set; } = new List<Fault_Photo>();
        [ForeignKey("ModelID")]
        public Model Model { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
        [ForeignKey("OperatorID")]
        public ApplicationUser Operator { get; set; }
        [ForeignKey("TechnicianID")]
        public ApplicationUser Technician { get; set; }
        
        public string lat { get; set; }
        public string lng { get; set; }
    }
}
