namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a72 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faults", "Address", c => c.String());
            DropColumn("dbo.Faults", "Location");
            DropColumn("dbo.Faults", "Adress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faults", "Adress", c => c.String());
            AddColumn("dbo.Faults", "Location", c => c.String(nullable: false));
            DropColumn("dbo.Faults", "Address");
        }
    }
}
