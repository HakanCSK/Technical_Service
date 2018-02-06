using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechnicalService.BLL.Account;
using TechnicalService.BLL.Settings;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Entities.Site;
using TechnicalService.Entity.Enums;
using TechnicalService.Entity.IdentityModels;
using TechnicalService.Entity.ViewsModel;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public async Task<ActionResult> Index()
        {
            
            return View();
        }
     

            public PartialViewResult HeaderAreaResult()
        {
            
            return PartialView("_PartialHeader");
        }
        public PartialViewResult SliderAreaResult()
        {
            var model =  new SliderRepo().GetAll();
            return PartialView("_PartialSlider",model);
        }
        public PartialViewResult FooterAreaResult()
        {

            return PartialView("_PartialFooter");
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}