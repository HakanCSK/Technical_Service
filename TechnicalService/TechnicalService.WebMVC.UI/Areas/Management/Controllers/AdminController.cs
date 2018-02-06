using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechnicalService.BLL.Account;
using TechnicalService.Entity.ViewsModel.ManagementViewModels;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        // GET: Management/Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UserManager()

        {
            if (TempData["Result"] != null)
            {
                ViewBag.Result = TempData["Result"];
                TempData.Remove("Result");
            }
            var Users = GetAllUsers();
            ViewBag.Rolles = RoleSelectList();
            var rol = RoleSelectList();
            
            return View(Users);

        }
   
        [HttpPost]
        public ActionResult DeleteUser(string id)
        {
            bool result = MembershipTools.DeleteUser(id);
            if (result)
                TempData["Result"] = "Kullanıcı silindi";
            else
                TempData["Result"] = "Kullanıcı Silinirken Hata Oluştu!!";
            return RedirectToAction("UserManager");
        }

    }
}