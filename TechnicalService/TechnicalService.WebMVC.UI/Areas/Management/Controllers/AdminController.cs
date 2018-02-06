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
            var Users = GetAllUsers();
            ViewBag.Rolles = RoleSelectList();
            var rol = RoleSelectList();
            
            return View(Users);

        }
         [HttpPost]
        public ActionResult UserManager(string id)
        {
            if (id != null)
            {
                bool result = MembershipTools.DeleteUser(id);
                if (result)
                    ViewBag.Result = "Kullanıcı silindi";
                else
                    ViewBag.Result = "Kullanıcı Silinirken Hata Oluştu!!";
            }
            var Users = GetAllUsers();
            ViewBag.Rolles = RoleSelectList();

            return View(Users);

        }

    }
}