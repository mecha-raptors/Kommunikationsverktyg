namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Placards : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlacardModels", "Category_CategoryModelId", "dbo.CategoryModels");
            DropIndex("dbo.PlacardModels", new[] { "Category_CategoryModelId" });
            RenameColumn(table: "dbo.PlacardModels", name: "PlacardTypeModel_PlacardTypeModelId", newName: "Type_PlacardTypeModelId");
            RenameIndex(table: "dbo.PlacardModels", name: "IX_PlacardTypeModel_PlacardTypeModelId", newName: "IX_Type_PlacardTypeModelId");
            DropColumn("dbo.PlacardModels", "Category_CategoryModelId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlacardModels", "Category_CategoryModelId", c => c.Int());
            RenameIndex(table: "dbo.PlacardModels", name: "IX_Type_PlacardTypeModelId", newName: "IX_PlacardTypeModel_PlacardTypeModelId");
            RenameColumn(table: "dbo.PlacardModels", name: "Type_PlacardTypeModelId", newName: "PlacardTypeModel_PlacardTypeModelId");
            CreateIndex("dbo.PlacardModels", "Category_CategoryModelId");
            AddForeignKey("dbo.PlacardModels", "Category_CategoryModelId", "dbo.CategoryModels", "CategoryModelId");
        }
    }
}
