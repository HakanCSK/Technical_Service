using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Site
{
    [Table("Headers")]
    class Header: BaseEntity<int>
    {
        public string TopText { get; set; }
        public string LogoUrl { get; set; }
        public string SmallLogoUrl { get; set; }



    }
}
