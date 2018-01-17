using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Entities.Product;
using TechnicalService.Entity.Entities.Survey;

namespace TechnicalService.BLL.Repository
{
    public class Repository
    {
        public class FaultRepo : RepositoryBase<Fault, int> { }
        public class FaultPhotoRepo : RepositoryBase<Fault_Photo, int> { }
        public class FaultStatusRepo : RepositoryBase<Fault_Status, int> { }


        public class BrandRepo : RepositoryBase<Brand, int> { }
        public class CategoryRepo : RepositoryBase<Category, int> { }
        public class ModelRepo : RepositoryBase<Model, int> { }


        public class SurveyRepo : RepositoryBase<Survey, int> { }
        public class SurveyQuestionRepo : RepositoryBase<Survey_Question, int> { }
        public class SurveyResponseRepo : RepositoryBase<Survey_Response, int> { }





    }
}
