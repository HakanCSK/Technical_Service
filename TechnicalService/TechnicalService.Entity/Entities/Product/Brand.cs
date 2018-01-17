using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalService.Entity.Entities.Product
{
    public class Brand:BaseEntity<int>
    {
        public string BrandName { get; set; }
        public List<Model> ModelList { get; set; }
    }
}
