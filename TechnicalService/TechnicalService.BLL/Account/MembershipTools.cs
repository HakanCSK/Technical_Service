﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TechnicalService.DAL;
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
        public static string GetUserName(string id)
        {
            var userManager = NewUserManager();
            var user = userManager.FindById(id);
            if (user != null)
                return user.UserName;
            return "Null";
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
