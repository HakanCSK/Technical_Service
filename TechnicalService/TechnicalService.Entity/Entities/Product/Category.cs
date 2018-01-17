using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Product
{
    [Table("Categories")]
    public class Category:BaseEntity<int>
    {
       
        [StringLength(25)]
        public string CategoryName { get; set; }

        public List<Model> ModelList { get; set; }

    }
}
