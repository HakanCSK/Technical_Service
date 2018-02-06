using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechnicalService.DAL;
using TechnicalService.Entity.Enums;
using TechnicalService.Entity.IdentityModels;

namespace TechnicalService.BLL.Account
{
    public class MembershipTools
    {
        public static UserStore<ApplicationUser> NewUserStore()
        {
            return new UserStore<ApplicationUser>(new MyContext());
        }
        public static UserManager<ApplicationUser> NewUserManager()
        {
            return new UserManager<ApplicationUser>(NewUserStore());
        }
        public static RoleStore<ApplicationRole> NewRoleStore()
        {
            return new RoleStore<ApplicationRole>(new MyContext());
        }
        public static RoleManager<ApplicationRole> NewRoleManager()
        {
            return new RoleManager<ApplicationRole>(NewRoleStore());
        }

        public static string GetRoleName(string roleID)
        {
            var roleManager = NewRoleManager();
            return roleManager.FindById(roleID).Name;

        }
        public static void SetRole(string ID,string RoleID)
        {
            var roleManager = NewRoleManager();
            var role=roleManager.FindById(RoleID);
            var userManager = NewUserManager();
            var user=userManager.FindById(ID);
            user.Roles.Clear();
            userManager.AddToRole(ID, role.Name);

        }
        public static List<ApplicationUser> GetTechnician()
        {
            var role=NewRoleManager().FindByName("Teknisyen").Id;
             return NewUserManager().Users.Where(x => x.Roles.Any(r=>r.RoleId==role)).ToList();

        }

        public static string GetUserName(string id)
        {
            var userManager = NewUserManager();
            var user = userManager.FindById(id);
            if (user != null)
                return user.UserName;
            return "Null";
        }
      public static  bool DeleteUser(string id)
        {
            var userManager = NewUserManager();
            //var user = userManager.FindById(id);
            if (userManager.IsInRole(id, "Admin"))
            {
                return false;
            }
            //user.Roles.Clear();
            userManager.AddToRole(id, IdentityRoles.Deleted.ToString());
            return true;
        }
        public static string GetUserFullName(string id)
        {
            var userManager = NewUserManager();
            var user=userManager.FindById(id);
            return user.Name + " " + user.SurName;
        }


        public static string GetNameSurname()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id))
                return "Ziyaretçi";
            else
            {
                var userManager = NewUserManager();
                var user = userManager.FindById(id);
                string nameSurname = string.Format("{0} {1}.", user.Name, user.SurName.Substring(0, 1));
                return nameSurname;
            }
        }
        public  static    async  Task<Boolean>   UpdateEmailAsync(string ID,string name)
        {
            var userStore = MembershipTools.NewUserStore();
            var userManager = NewUserManager();
            var user = userManager.FindById(ID);
            user.Name = name;

         
            try
            {
                await userManager.UpdateAsync(user);
            

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
        public static ApplicationUser GetUser()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            if (string.IsNullOrEmpty(id))
                return null;
            else
            {
                var userManager = NewUserManager();
                var user = userManager.FindById(id);
                return user;
            }
        }

    }
}
