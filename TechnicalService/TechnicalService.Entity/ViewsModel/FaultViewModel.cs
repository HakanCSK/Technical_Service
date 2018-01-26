using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechnicalService.Entity.Entities.Fault;

namespace TechnicalService.Entity.ViewsModel
{
    public class FaultViewModel
    {
      
        [Display(Name = "Kategori")]
        public int? CategoryID { get; set; }

        
        [Display(Name = "Marka")]
        public int? BrandID { get; set; }

        [Required(ErrorMessage = "Model seçilmesi zorunludur!")]
        [Display(Name = "Model")]
        public int ModelID { get; set; }

        [Required(ErrorMessage = "Başlık girilmesi zorunludur!")]
        [Display(Name = "Başlık")]
        [StringLength(80, MinimumLength = 5, ErrorMessage = "Başlık en az 5 en fazla 80 karakter olmalıdır!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Açıklama girilmesi zorunludur!")]
        [Display(Name = "Açıklama")]
        [StringLength(maximumLength:500, MinimumLength = 10, ErrorMessage = "Açıklama en az 10 en fazla 500 karakter olmalıdır!")]
        public string Description { get; set; }
        public string UserID { get; set; }
       
        public string Report { get; set; }
        public string InvoiceCode { get; set; }

        [Display(Name = "Rezervasyon Tarihi")]

        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [Required(ErrorMessage = "Adres açıklaması girilmesi zorunludur!")]
        [Display(Name = "Adres Açıklaması")]
        public string AddressDescription { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public List<Fault_Status> Statuses { get; set; } = new List<Fault_Status>();
        public List<HttpPostedFileBase> Fault_Photos { get; set; } = new List<HttpPostedFileBase>();
    }
}
