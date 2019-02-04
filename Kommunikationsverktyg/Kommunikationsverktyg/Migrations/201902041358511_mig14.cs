namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryModels",
                c => new
                    {
                        CategoryModelId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.CategoryModelId);
            
            AddColumn("dbo.FormalBlogModels", "CategoryModelId", c => c.Int(nullable: false));
            CreateIndex("dbo.FormalBlogModels", "CategoryModelId");
            AddForeignKey("dbo.FormalBlogModels", "CategoryModelId", "dbo.CategoryModels", "CategoryModelId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormalBlogModels", "CategoryModelId", "dbo.CategoryModels");
            DropIndex("dbo.FormalBlogModels", new[] { "CategoryModelId" });
            DropColumn("dbo.FormalBlogModels", "CategoryModelId");
            DropTable("dbo.CategoryModels");
        }
    }
}
