using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        // GET: Management/Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}