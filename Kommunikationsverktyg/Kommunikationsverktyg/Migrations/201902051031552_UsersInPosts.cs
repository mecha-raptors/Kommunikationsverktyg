namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersInPosts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FormalBlogModels", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.FormalBlogModels", new[] { "Id" });
            AddColumn("dbo.FormalBlogModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.FormalBlogModels", "User_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "FormalBlogModel_FormalBlogModelId", c => c.Int());
            AlterColumn("dbo.FormalBlogModels", "Id", c => c.String());
            CreateIndex("dbo.FormalBlogModels", "ApplicationUser_Id");
            CreateIndex("dbo.FormalBlogModels", "User_Id");
            CreateIndex("dbo.AspNetUsers", "FormalBlogModel_FormalBlogModelId");
            AddForeignKey("dbo.AspNetUsers", "FormalBlogModel_FormalBlogModelId", "dbo.FormalBlogModels", "FormalBlogModelId");
            AddForeignKey("dbo.FormalBlogModels", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FormalBlogModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FormalBlogModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FormalBlogModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "FormalBlogModel_FormalBlogModelId", "dbo.FormalBlogModels");
            DropIndex("dbo.AspNetUsers", new[] { "FormalBlogModel_FormalBlogModelId" });
            DropIndex("dbo.FormalBlogModels", new[] { "User_Id" });
            DropIndex("dbo.FormalBlogModels", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.FormalBlogModels", "Id", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "FormalBlogModel_FormalBlogModelId");
            DropColumn("dbo.FormalBlogModels", "User_Id");
            DropColumn("dbo.FormalBlogModels", "ApplicationUser_Id");
            CreateIndex("dbo.FormalBlogModels", "Id");
            AddForeignKey("dbo.FormalBlogModels", "Id", "dbo.AspNetUsers", "Id");
        }
    }
}
