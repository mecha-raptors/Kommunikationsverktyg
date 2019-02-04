namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrationes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LikeModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormalPosts_FormalBlogModelId = c.Int(),
                        Users_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FormalBlogModels", t => t.FormalPosts_FormalBlogModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.Users_Id)
                .Index(t => t.FormalPosts_FormalBlogModelId)
                .Index(t => t.Users_Id);
            
            AddColumn("dbo.FormalBlogModels", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LikeModels", "Users_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.LikeModels", "FormalPosts_FormalBlogModelId", "dbo.FormalBlogModels");
            DropIndex("dbo.LikeModels", new[] { "Users_Id" });
            DropIndex("dbo.LikeModels", new[] { "FormalPosts_FormalBlogModelId" });
            DropColumn("dbo.FormalBlogModels", "Likes");
            DropTable("dbo.LikeModels");
        }
    }
}
