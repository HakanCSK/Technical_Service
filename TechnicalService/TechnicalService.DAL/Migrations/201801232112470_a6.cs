namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sliders", "Title", c => c.String());
            AddColumn("dbo.Sliders", "Description", c => c.String());
            DropColumn("dbo.Sliders", "Text");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sliders", "Text", c => c.String());
            DropColumn("dbo.Sliders", "Description");
            DropColumn("dbo.Sliders", "Title");
        }
    }
}
