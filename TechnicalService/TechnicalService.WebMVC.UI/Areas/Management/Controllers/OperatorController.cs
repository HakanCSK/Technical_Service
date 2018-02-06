using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Web.Mvc;
using TechnicalService.BLL.Account;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{
    [Authorize(Roles = "Operatör,Admin")]
    public class OperatorController : Controller
    {
        // GET: Management/Operator
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult FaultList(int type=0)
        {
            ViewBag.Technician = MembershipTools.GetTechnician();
            if (type == 0)
                ViewBag.OperatorID = null;
            else
            ViewBag.OperatorID = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var model = new FaultRepo().GetAll();
            return View(model);
        }
        public ActionResult Details(int id)
        {
           
           var faultRepo= new FaultRepo();
            var Fault=faultRepo.GetById(id);

            return View(Fault);
        }


    }
}