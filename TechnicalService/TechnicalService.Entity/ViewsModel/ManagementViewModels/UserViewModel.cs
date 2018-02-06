using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.ViewsModel.ManagementViewModels
{
    public class UserViewModel
    {
        public string ID { get; set; }
        [Required]
        [Display(Name ="Ad")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Soyad")]
        public string SurName { get; set; }

        [Display(Name = "Kayıt Tarihi")]
        public DateTime RegisterDate { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public Boolean EmailConfirmed { get; set; }
        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Rolü")]
        public string RoleID { get; set; }

        public string RoleName { get; set; }
    }
}
