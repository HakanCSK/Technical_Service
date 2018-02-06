using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalService.BLL.Account;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{
    [Authorize(Roles = "Teknisyen")]
    public class TechnicianController : Controller
    {
        // GET: Management/Technician
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaultList()
        {

            var model = new FaultRepo().GetAll().Where(x => x.TechnicianID == MembershipTools.GetUser().Id).ToList();
            return View(model);
        }
     
        public ActionResult Edit(int id)
        {
            var model = new FaultRepo().GetById(id);

            return View(model);
        }
    }
}