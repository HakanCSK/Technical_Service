namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Models",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BrandID = c.Int(nullable: false),
                        CategoryID = c.Int(nullable: false),
                        ModelName = c.String(maxLength: 25),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Brands", t => t.BrandID, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.BrandID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(maxLength: 25),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Fault_Photos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        URL = c.String(nullable: false),
                        FaultID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Faults", t => t.FaultID, cascadeDelete: true)
                .Index(t => t.FaultID);
            
            CreateTable(
                "dbo.Faults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        UserID = c.String(maxLength: 128),
                        OperatorID = c.String(maxLength: 128),
                        TechnicianID = c.String(maxLength: 128),
                        Title = c.String(nullable: false),
                        Report = c.String(),
                        InvoiceCode = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        Location = c.String(nullable: false),
                        Adress = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Models", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.OperatorID)
                .ForeignKey("dbo.AspNetUsers", t => t.TechnicianID)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.ProductID)
                .Index(t => t.UserID)
                .Index(t => t.OperatorID)
                .Index(t => t.TechnicianID)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(maxLength: 25),
                        SurName = c.String(maxLength: 25),
                        RegisterDate = c.DateTime(nullable: false),
                        ActivationCode = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Survey_Responses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quesetion_Response = c.Int(),
                        QuestionID = c.Int(nullable: false),
                        UserID = c.String(maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Survey_Questions", t => t.QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.QuestionID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Survey_Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuestionsText = c.String(nullable: false),
                        SurveyID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Surveys", t => t.SurveyID, cascadeDelete: true)
                .Index(t => t.SurveyID);
            
            CreateTable(
                "dbo.Surveys",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SurveyName = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Fault_Statuses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FaultID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Faults", t => t.FaultID, cascadeDelete: true)
                .Index(t => t.FaultID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 200),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Faults", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Faults", "TechnicianID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fault_Statuses", "FaultID", "dbo.Faults");
            DropForeignKey("dbo.Fault_Photos", "FaultID", "dbo.Faults");
            DropForeignKey("dbo.Faults", "OperatorID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Survey_Responses", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Survey_Questions", "SurveyID", "dbo.Surveys");
            DropForeignKey("dbo.Survey_Responses", "QuestionID", "dbo.Survey_Questions");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Faults", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Faults", "ProductID", "dbo.Models");
            DropForeignKey("dbo.Models", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.Models", "BrandID", "dbo.Brands");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Fault_Statuses", new[] { "FaultID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.Survey_Questions", new[] { "SurveyID" });
            DropIndex("dbo.Survey_Responses", new[] { "UserID" });
            DropIndex("dbo.Survey_Responses", new[] { "QuestionID" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Faults", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Faults", new[] { "TechnicianID" });
            DropIndex("dbo.Faults", new[] { "OperatorID" });
            DropIndex("dbo.Faults", new[] { "UserID" });
            DropIndex("dbo.Faults", new[] { "ProductID" });
            DropIndex("dbo.Fault_Photos", new[] { "FaultID" });
            DropIndex("dbo.Models", new[] { "CategoryID" });
            DropIndex("dbo.Models", new[] { "BrandID" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Fault_Statuses");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Surveys");
            DropTable("dbo.Survey_Questions");
            DropTable("dbo.Survey_Responses");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Faults");
            DropTable("dbo.Fault_Photos");
            DropTable("dbo.Categories");
            DropTable("dbo.Models");
            DropTable("dbo.Brands");
        }
    }
}
