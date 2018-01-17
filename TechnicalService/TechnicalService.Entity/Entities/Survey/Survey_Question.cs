using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Survey
{
    [Table("Survey_Questions")]
    public class Survey_Question : BaseEntity<int>
    {
        [Required]
        public string QuestionsText { get; set; }
        public int SurveyID { get; set; }

        public List<Survey_Response> Responses { get; set; } = new List<Survey_Response>();
        [ForeignKey("SurveyID")]
        public Survey Survey { get; set; }
    }
}
