using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Survey
{
    [Table("Surveys")]
    public class Survey : BaseEntity<int>
    {
        public string SurveyName { get; set; }
        public List<Survey_Question> Questions { get; set; } = new List<Survey_Question>();
        public override string ToString()
        {
            return SurveyName;
        }
    }
}
