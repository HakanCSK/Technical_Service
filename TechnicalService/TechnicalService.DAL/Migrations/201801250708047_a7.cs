namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a7 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Faults", "TransactionDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Faults", "lat", c => c.String());
            AddColumn("dbo.Faults", "lng", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Faults", "lng");
            DropColumn("dbo.Faults", "lat");
            DropColumn("dbo.Faults", "TransactionDate");
        }
    }
}
