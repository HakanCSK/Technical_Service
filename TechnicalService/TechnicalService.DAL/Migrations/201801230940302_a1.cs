namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sliders", "File_ID", "dbo.Files");
            DropIndex("dbo.Sliders", new[] { "File_ID" });
            AddColumn("dbo.Sliders", "PhotoUrl", c => c.String());
            DropColumn("dbo.Sliders", "File_ID");
            DropTable("dbo.Files");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        SliderID = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Sliders", "File_ID", c => c.Int());
            DropColumn("dbo.Sliders", "PhotoUrl");
            CreateIndex("dbo.Sliders", "File_ID");
            AddForeignKey("dbo.Sliders", "File_ID", "dbo.Files", "ID");
        }
    }
}
