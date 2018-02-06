using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web.Mvc;

using TechnicalService.BLL.Account;
using TechnicalService.Entity.IdentityModels;
using TechnicalService.Entity.ViewsModel.ManagementViewModels;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Areas.Management.Controllers
{

    public class BaseController : Controller
    {
        [NonAction]
        public List<SelectListItem> RoleSelectList()
        {


            var RoleList = MembershipTools.NewRoleManager().Roles.ToList();
            var Roles = new List<SelectListItem>();

            RoleList.ForEach(x =>
            Roles.Add(new SelectListItem
            {
                Text = x.Name,
                Value = x.Id
            })
            );
            return Roles;
        }
        [NonAction]
        [Authorize(Roles = "Admin")]
        public List<UserViewModel> GetAllUsers()
        {
            var UserList = new List<UserViewModel>();
            var userManager = MembershipTools.NewUserManager();

            List<ApplicationUser> list= userManager.Users.ToList();
            foreach(var item in list)
            {
               
                    UserList.Add(new UserViewModel()
                    {
                        Name = item.Name,
                        SurName = item.SurName,
                        Email = item.Email,
                        UserName = item.UserName,
                        ID = item.Id,
                        RoleID = item.Roles.First()?.RoleId,
                        EmailConfirmed = item.EmailConfirmed,
                        PhoneNumber = item.PhoneNumber,
                        RegisterDate = item.RegisterDate,
                        RoleName=MembershipTools.GetRoleName(item.Roles.First()?.RoleId)
                    });
                

            }
                
            return UserList;
        }


      

        [HttpPost]
        public async System.Threading.Tasks.Task<JsonResult> UserEdit(string value, string pk, string name)
        {
            try
            {
                var man = MembershipTools.NewUserManager();
                var user = await man.FindByIdAsync(pk);
                PropertyInfo prop = null;
                foreach (PropertyInfo p in typeof(ApplicationUser).GetProperties())
                {
                    if (p.Name == name)
                    {
                        prop = p;
                        p.SetValue(user, Convert.ChangeType(value, p.PropertyType));
                    }
                }
                Microsoft.AspNet.Identity.IdentityResult result = await man.UpdateAsync(user);
                if (result != Microsoft.AspNet.Identity.IdentityResult.Success)
                {

                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return this.Json(new
                    {
                        message = result.Errors
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return this.Json(new
                {
                    message = ex
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json(new
            {
                success = true,
            }, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public string UpdateFault(string latlng,string technician,string id,string address,string title,string description)
        {
            var latlngArray = latlng.Split(',');
            var faultrepo= new FaultRepo();
            var fault=faultrepo.GetById(Convert.ToInt32(id));
            fault.TechnicianID = technician;
            fault.lat=latlngArray[0];
            fault.lng = latlngArray[1];
            fault.Address = address;
            fault.Description = description;
            fault.OperatorID=System.Web.HttpContext.Current.User.Identity.GetUserId();
            fault.Title = title;
            try
            {

                faultrepo.Update();
            }
            catch (Exception ex)
            {
                return ex.Message;    
            }
            return "ok";




        }


        [HttpPost]
        public void UserRoleEdit(string value, string pk)
        {
            MembershipTools.SetRole(pk, value);

          
        }





    }
}