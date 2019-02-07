namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchatModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormalBlogModels", "CategoryModelId", "dbo.CategoryModels");
            DropIndex("dbo.FormalBlogModels", new[] { "CategoryModelId" });
            RenameColumn(table: "dbo.FormalBlogModels", name: "CategoryModelId", newName: "Category_CategoryModelId");
            AlterColumn("dbo.FormalBlogModels", "Category_CategoryModelId", c => c.Int());
            CreateIndex("dbo.FormalBlogModels", "Category_CategoryModelId");
            AddForeignKey("dbo.FormalBlogModels", "Category_CategoryModelId", "dbo.CategoryModels", "CategoryModelId");
            DropColumn("dbo.FormalBlogModels", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FormalBlogModels", "Id", c => c.String());
            DropForeignKey("dbo.FormalBlogModels", "Category_CategoryModelId", "dbo.CategoryModels");
            DropIndex("dbo.FormalBlogModels", new[] { "Category_CategoryModelId" });
            AlterColumn("dbo.FormalBlogModels", "Category_CategoryModelId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.FormalBlogModels", name: "Category_CategoryModelId", newName: "CategoryModelId");
            CreateIndex("dbo.FormalBlogModels", "CategoryModelId");
            AddForeignKey("dbo.FormalBlogModels", "CategoryModelId", "dbo.CategoryModels", "CategoryModelId", cascadeDelete: true);
        }
    }
}
