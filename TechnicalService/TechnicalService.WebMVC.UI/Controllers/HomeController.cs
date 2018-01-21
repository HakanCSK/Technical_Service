using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalService.WebMVC.UI.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult HeaderAreaResult()
        {
            
            return PartialView("_PartialHeader");
        }
        public PartialViewResult FooterAreaResult()
        {

            return PartialView("_PartialFooter");
        }


    }
}