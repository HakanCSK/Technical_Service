using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.DAL;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Entities.Product;
using TechnicalService.Entity.Entities.Site;
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
        public class ModelRepo : RepositoryBase<Model, int> {
            public List<Model> ModelList(int CatID,int BrandID)
            {
                dbContext = new MyContext();
                if (CatID == -1)
                
                    return dbContext.Models.Where(x => x.BrandID == BrandID ).ToList();
                else if(BrandID==-1)
                    return dbContext.Models.Where(x=>x.CategoryID == CatID).ToList();


                return dbContext.Models.Where(x => x.BrandID == BrandID && x.CategoryID == CatID).ToList();
            }

        }


        public class SurveyRepo : RepositoryBase<Survey, int> { }
        public class SurveyQuestionRepo : RepositoryBase<Survey_Question, int> { }
        public class SurveyResponseRepo : RepositoryBase<Survey_Response, int> { }

        public class SliderRepo : RepositoryBase<Slider, int> { }
        





    }
}
