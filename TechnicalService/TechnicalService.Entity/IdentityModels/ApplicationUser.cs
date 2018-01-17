using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Entities.Survey;

namespace TechnicalService.Entity.IdentityModels
{

    public class ApplicationUser : IdentityUser
    {
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(25)]
        public string SurName { get; set; }

        public DateTime RegisterDate { get; set; } = DateTime.Now;

        public string ActivationCode { get; set; }

        public List<Survey_Response> Responses { get; set; } = new List<Survey_Response>();

        public List<Fault> Faults { get; set; } = new List<Fault>();
    }
}
