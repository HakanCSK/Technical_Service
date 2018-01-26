﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechnicalService.BLL.Account;
using TechnicalService.BLL.Settings;
using TechnicalService.Entity.Enums;
using TechnicalService.Entity.IdentityModels;
using TechnicalService.Entity.ViewsModel;

namespace TechnicalService.WebMVC.UI.Controllers
{
    public class AccountController : Controller
    {


        // GET: Account
        [AllowAnonymous]
        
        public virtual ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
              return  RedirectToAction("Profile", "Account");
            ViewBag.Action = this.ControllerContext.RouteData.Values["action"].ToString();
            ViewBag.Controller = this.ControllerContext.RouteData.Values["controller"].ToString();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var userManager = MembershipTools.NewUserManager();

            var checkUser = userManager.FindByName(model.Username);
            if (checkUser != null)
            {
                ModelState.AddModelError(string.Empty, "Bu kullanıcı adı daha önceden kayıt edilmiş");
                return View(model);
            }

            var activationCode = Guid.NewGuid().ToString().Replace("-", "");
            var user = new ApplicationUser()
            {
                Name = model.Name,
                SurName = model.Surname,
                Email = model.Email,
                UserName = model.Username,
                ActivationCode = activationCode
            };

            var sonuc = userManager.Create(user, model.Password);
            if (sonuc.Succeeded)
            {
                if (userManager.Users.Count() == 1)
                    userManager.AddToRole(user.Id, IdentityRoles.Admin.ToString());
                else
                    userManager.AddToRole(user.Id, IdentityRoles.Passive.ToString());

                string siteUrl = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                                 (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port);
                await SiteSettings.SendMail(new MailModel()
                {
                    To = user.Email,
                    Subject = "UyelikDB - Aktivasyon",
                    Message = $"Merhaba {user.Name} {user.SurName} <br/>Hesabınızı aktifleştirmek için <b><a href='{siteUrl}/Account/Activation?code={activationCode}'>Aktivasyon Kodu</a></b> tıklayınız."
                });

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı kayıt işleminde hata oluştu!");
                return View(model);
            }
        }
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile", "Account");
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (HttpContext.User.IsInRole("Passive"))
                {
                    ViewBag.sonuc = "Sistemi kullanabilmeniz için eposta adresinizi aktifleştirmeniz gerekmektedir.";
                }

                if (ReturnUrl != null)
                {
                    var url = ReturnUrl.Split('/');

                    // admin/kullaniciduzenle/5
                    // admin/kullanicilar
                    if (url[1].ToLower().Contains("admin"))
                    {
                        ViewBag.sonuc = "Bu alana yönetici hesabınızla girebilirsiniz. Lütfen yönetici bilgilerinizle giriş yapınız.";
                    }
                }
            }
            var model = new LoginViewModel() { ReturnUrl = ReturnUrl };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var userManager = MembershipTools.NewUserManager();
            var user = await userManager.FindAsync(model.Username, model.Password);
            if (user == null)
            {
                if(userManager.FindByName(model.Username)==null)
                ModelState.AddModelError(string.Empty, "Böyle bir kullanıcı bulunamadı");
                else
                ModelState.AddModelError(string.Empty, "Şifre hatalı");
                return View(model);
            }
            else
            {
                var roleName = MembershipTools.NewRoleManager().FindById(user.Roles.First().RoleId).Name;
                if (roleName == IdentityRoles.Passive.ToString())
                    {
                        ViewBag.sonuc = "Sistemi kullanabilmeniz için eposta adresinizi aktifleştirmeniz gerekmektedir.";
                    return View();
                    }

                    if (model.ReturnUrl != null)
                    {
                        var url = model.ReturnUrl.Split('/');

                        // admin/kullaniciduzenle/5
                        // admin/kullanicilar
                        if (url[1].ToLower().Contains("admin"))
                        {
                            ViewBag.sonuc = "Bu alana yönetici hesabınızla girebilirsiniz. Lütfen yönetici bilgilerinizle giriş yapınız.";
                        }
                    }
                
               
            }
            var authManager = HttpContext.GetOwinContext().Authentication;
            var userIdentity = await userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            authManager.SignIn(new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            }, userIdentity);
            if (string.IsNullOrEmpty(model.ReturnUrl))
                return RedirectToAction("Index", "Home");
            try
            {
                var url = model.ReturnUrl.Split('/');
                if (url.Length == 4)
                    return RedirectToAction(url[2], url[1], new { id = url[3] });
                else
                    return RedirectToAction(url[2], url[1]);
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Profile()
        {
            var user = MembershipTools.GetUser();

            return View(new ProfileViewModel()
            {
                Email = user.Email,
                Name = user.Name,
                Surname = user.SurName,
                Username = user.UserName
            });
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Profile(ProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                var userStore = MembershipTools.NewUserStore();
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindById(HttpContext.User.Identity.GetUserId());
                user.Name = model.Name;
                user.SurName = model.Surname;
                user.Email = model.Email;
                if (userManager.FindByName(model.Username) != null && model.Username != user.UserName)
                {
                    ViewBag.sonuc = "Bu kullanıcı adı zaten kullanılıyor.";
                    return View(model);
                }
                user.UserName = model.Username;
              
                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                


                ViewBag.sonuc = "Bilgileriniz güncellenmiştir";

                var model2 = new ProfileViewModel()
                {
                    Email = user.Email,
                    Name = user.Name,
                    Surname = user.SurName,
                    Username = user.UserName
                };
                return View(model2);
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Güncelleştirme işleminde bir hata oluştu. "+ex.Message;
                return View(model);
            }
        }

        [Authorize]
        public ActionResult UpdatePassword()
        {
            return View();


        }
        

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdatePassword(ProfileViewModel model)
        {
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                ModelState.AddModelError(string.Empty, "Şifreler uyuşmuyor");
                return View("UpdatePassword");
            }
            try
            {
                var userStore = MembershipTools.NewUserStore();
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = userManager.FindById(HttpContext.User.Identity.GetUserId());
                user = userManager.Find(user.UserName, model.OldPassword);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Mevcut şifreniz yanlış girildi");
                    return View("UpdatePassword");
                }
                await userStore.SetPasswordHashAsync(user, userManager.PasswordHasher.HashPassword(model.NewPassword));
                await userStore.UpdateAsync(user);
                await userStore.Context.SaveChangesAsync();
                HttpContext.GetOwinContext().Authentication.SignOut();
                return RedirectToAction("UpdatePassword");
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Güncelleştirme işleminde bir hata oluştu. " + ex.Message;
                throw;
            }

            return View();
        }

        public ActionResult RecoverPassword()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RecoverPassword(string email)
        {
            var userStore = MembershipTools.NewUserStore();
            var userManager = new UserManager<ApplicationUser>(userStore);
            try
            {
                var sonuc = userStore.Context.Set<ApplicationUser>().FirstOrDefault(x => x.Email == email);
                if (sonuc == null)
                {
                    ViewBag.sonuc = "E mail adresiniz sisteme kayıtlı değil";
                    return View();
                }
                var randomPass = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 6);
                await userStore.SetPasswordHashAsync(sonuc, userManager.PasswordHasher.HashPassword(randomPass));
                await SiteSettings.SendMail(new MailModel()
                {
                    To = sonuc.Email,
                    Subject = "Şifreniz Değişti",
                    Message = $"Merhaba {sonuc.Name} {sonuc.SurName} <br/>Yeni Şifreniz : <b>{randomPass}</b>"
                });
                ViewBag.sonuc = "Email adresinize yeni şifreniz gönderilmiştir";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Sistemsel bir hata oluştu. Tekrar deneyiniz";
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Activation(string code)
        {
            var userStore = MembershipTools.NewUserStore();
            var sonuc = userStore.Context.Set<ApplicationUser>().FirstOrDefault(x => x.ActivationCode == code);
            if (sonuc == null)
            {
                ViewBag.sonuc = "Aktivasyon işlemi  başarısız";
                return View();
            }

            if (sonuc.EmailConfirmed)
            {
                ViewBag.sonuc = "E-Posta adresiniz zaten onaylı";
                return View();
            }
            sonuc.EmailConfirmed = true;
            await userStore.UpdateAsync(sonuc);
            await userStore.Context.SaveChangesAsync();

            var userManager = MembershipTools.NewUserManager();
            var roleId = userManager.FindById(sonuc.Id)?.Roles.First().RoleId;
            var roleName = MembershipTools.NewRoleManager().FindById(roleId).Name;
            if (roleName == IdentityRoles.Passive.ToString())
            {
                userManager.RemoveFromRole(sonuc.Id, IdentityRoles.Passive.ToString());
                userManager.AddToRole(sonuc.Id, IdentityRoles.User.ToString());
            }
            ViewBag.sonuc = $"Merhaba {sonuc.Name} {sonuc.SurName}  Aktivasyon işleminiz başarılı";
            await SiteSettings.SendMail(new MailModel()
            {
                To = sonuc.Email,
                Message = ViewBag.sonuc.ToString(),
                Subject = "Uyelik - Aktivasyon"
            });
            return View();
        }
    }
}
