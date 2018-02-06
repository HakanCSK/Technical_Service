using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{
    [Authorize(Roles = "Technician")]
    public class TechnicianController : Controller
    {
        // GET: Management/Technician
        public ActionResult Index()
        {
            return View();
        }
    }
}