using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.IdentityModels;

namespace TechnicalService.Entity.Entities.Survey
{
    [Table("Survey_Responses")]
    public class Survey_Response : BaseEntity<int>
    {
        public int? Quesetion_Response { get; set; }
        public int QuestionID { get; set; }
        public string UserID { get; set; }
        [ForeignKey("QuestionID")]
        public Survey_Question Question { get; set; }
        [ForeignKey("UserID")]
        public ApplicationUser User { get; set; }
    }
}
