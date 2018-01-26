using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TechnicalService.BLL.Settings;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Enums;
using TechnicalService.Entity.ViewsModel;
using static TechnicalService.BLL.Repository.Repository;

namespace TechnicalService.WebMVC.UI.Controllers
{
    public class FaultController : BaseController
    {
        public ActionResult FaultReport()

        {
            var Categories = CategoriSelectList();
            var Brands = BrandSelectList();
            ViewBag.Brands = Brands;
            ViewBag.Categories = Categories;
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FaultReport(FaultViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var Categories = CategoriSelectList();
                var Brands = BrandSelectList();
                ViewBag.Brands = Brands;
                ViewBag.Categories = Categories;
                return View(model);
            }


            if (model == null)
                return RedirectToAction("FaultReport");
            Fault NewFault = new Fault()
            {
                 ModelID=model.ModelID,
                UserID = User.Identity.GetUserId(),
                Description = model.Description,
                Address = model.AddressDescription,
                Title = model.Title,
                lat = model.lat,
                lng = model.lng,
                InvoiceCode = model.InvoiceCode,
                AppointmentDate = model.TransactionDate
                
                 
            };
            try
            {
                new FaultRepo().Insert(NewFault);
                if (model.Fault_Photos.Any())
                {
                    foreach (var dosya in model.Fault_Photos)
                    {
                        if (dosya != null && dosya.ContentType.Contains("image") && dosya.ContentLength > 0)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(dosya.FileName);
                            string extName = Path.GetExtension(dosya.FileName);
                            fileName = SiteSettings.UrlFormatConverter(fileName);
                            fileName += Guid.NewGuid().ToString().Replace("-", "");
                            var directoryPath = Server.MapPath("~/Uploads/ArizaImage");
                            var filePath = Server.MapPath("~/Uploads/ArizaImage/") + fileName + extName;
                            if (!Directory.Exists(directoryPath))
                                Directory.CreateDirectory(directoryPath);
                            dosya.SaveAs(filePath);
                            ImageResize(400, 300, filePath);
                            new FaultPhotoRepo().Insert(new Fault_Photo()
                            {
                                FaultID = NewFault.ID,
                                URL = @"/Uploads/FaultImage/" + fileName + extName
                            });
                        }
                    }
                }
                Fault_Status NewFault_Status = new Fault_Status() //Ariza durumunu oluşturuldu olarak ekle
                {
                    Status = FaultStatus.Olusturuldu,
                    Description = "Arıza  oluşturuldu.",
                    FaultID = NewFault.ID
                };
                NewFault.Statuses.Add(NewFault_Status);
                new FaultRepo().Update();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.sonuc = "Arıza kaydedilirken beklenmeyen bir hata oluştu. > " + ex.Message;
                return RedirectToAction("FaultReport");
            }


        }

    }
}