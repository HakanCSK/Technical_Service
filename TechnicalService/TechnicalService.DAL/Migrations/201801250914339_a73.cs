namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a73 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Faults", "TransactionDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Faults", "TransactionDate", c => c.DateTime(nullable: false));
        }
    }
}
