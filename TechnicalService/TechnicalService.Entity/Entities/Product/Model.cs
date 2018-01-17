using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Product
{
    public class Model:BaseEntity<int>
    {
        public int BrandID { get; set; }
        public int CategoryID { get; set; }
        [StringLength(25)]
        public string ModelName { get; set; }

        [ForeignKey("BrandID")]
        public Brand Brand { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }



    }
}
