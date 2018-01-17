using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalService.Entity.Entities.Fault;
using TechnicalService.Entity.Entities.Product;
using TechnicalService.Entity.Entities.Survey;
using TechnicalService.Entity.IdentityModels;

namespace TechnicalService.DAL
{
    public class MyContext:IdentityDbContext<ApplicationUser>
    {
        public MyContext() : base("name=MyCon")
        {

        }

        public DbSet<Fault> Faults { get; set; }
        public DbSet<Fault_Photo> Fault_Photoss { get; set; }
        public DbSet<Fault_Status> Fault_Statusess { get; set; }


        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Model> Models { get; set; }


        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Survey_Question> Survey_Questionss { get; set; }
        public DbSet<Survey_Response> Survey_Responsess { get; set; }






    }
}
