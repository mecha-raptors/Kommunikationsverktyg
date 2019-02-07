namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LagtTillPlacards : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PlacardModels",
                c => new
                    {
                        PlacardId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Message = c.String(),
                        Title = c.String(),
                        Category_CategoryModelId = c.Int(),
                        User_Id = c.String(maxLength: 128),
                        PlacardTypeModel_PlacardTypeModelId = c.Int(),
                    })
                .PrimaryKey(t => t.PlacardId)
                .ForeignKey("dbo.CategoryModels", t => t.Category_CategoryModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .ForeignKey("dbo.PlacardTypeModels", t => t.PlacardTypeModel_PlacardTypeModelId)
                .Index(t => t.Category_CategoryModelId)
                .Index(t => t.User_Id)
                .Index(t => t.PlacardTypeModel_PlacardTypeModelId);
            
            CreateTable(
                "dbo.PlacardTypeModels",
                c => new
                    {
                        PlacardTypeModelId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.PlacardTypeModelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlacardModels", "PlacardTypeModel_PlacardTypeModelId", "dbo.PlacardTypeModels");
            DropForeignKey("dbo.PlacardModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.PlacardModels", "Category_CategoryModelId", "dbo.CategoryModels");
            DropIndex("dbo.PlacardModels", new[] { "PlacardTypeModel_PlacardTypeModelId" });
            DropIndex("dbo.PlacardModels", new[] { "User_Id" });
            DropIndex("dbo.PlacardModels", new[] { "Category_CategoryModelId" });
            DropTable("dbo.PlacardTypeModels");
            DropTable("dbo.PlacardModels");
        }
    }
}
