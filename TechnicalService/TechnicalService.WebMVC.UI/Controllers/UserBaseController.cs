using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Controllers
{
    public class UserBaseController : Controller
    {
        [NonAction]
        public void ImageResize(int width, int height, string path)
        {
            WebImage img = new WebImage(path);
            img.Resize(width, height, false);
           
            img.Save(path);
        }
        [NonAction]
        public List<SelectListItem> CategoriSelectList()
        {
            var CategoryList =  new CategoryRepo().GetAll();
            var Categories = new List<SelectListItem>();
           
            CategoryList.ForEach(x =>
            Categories.Add(new SelectListItem
            {
                Text = x.CategoryName,
                Value = x.ID.ToString()
            })
            );
            return Categories;
        }

        public List<SelectListItem> BrandSelectList()
        {
            var BrandList =  new BrandRepo().GetAll();
            var Brands = new List<SelectListItem>();

            BrandList.ForEach(x =>
            Brands.Add(new SelectListItem
            {
                Text = x.BrandName,
                Value = x.ID.ToString()
            })
            );
            return Brands;
        }


       



        [AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadModel(int CatId=-1,int BrandId=-1)
        {
            //Your Code For Getting Physicans Goes Here
            var ModelList = new ModelRepo().ModelList(CatId, BrandId);


            var phyData = ModelList.Select(m => new SelectListItem()
            {
                Text = m.ModelName,
                Value = m.ID.ToString(),
            });
            return Json(phyData, JsonRequestBehavior.AllowGet);
        }




    }
}