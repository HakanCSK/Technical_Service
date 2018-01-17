namespace TechnicalService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Faults", name: "ProductID", newName: "ModelID");
            RenameIndex(table: "dbo.Faults", name: "IX_ProductID", newName: "IX_ModelID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Faults", name: "IX_ModelID", newName: "IX_ProductID");
            RenameColumn(table: "dbo.Faults", name: "ModelID", newName: "ProductID");
        }
    }
}
