namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig44 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FileModels",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: true),
                        FilePath = c.String(),
                        InformalBlogModel_InformalBlogModelId = c.Int(),
                    })
                .PrimaryKey(t => t.FileId)
                .ForeignKey("dbo.InformalBlogModels", t => t.InformalBlogModel_InformalBlogModelId)
                .Index(t => t.InformalBlogModel_InformalBlogModelId);
            
            DropColumn("dbo.InformalBlogModels", "FilePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InformalBlogModels", "FilePath", c => c.String());
            DropForeignKey("dbo.FileModels", "InformalBlogModel_InformalBlogModelId", "dbo.InformalBlogModels");
            DropIndex("dbo.FileModels", new[] { "InformalBlogModel_InformalBlogModelId" });
            DropTable("dbo.FileModels");
        }
    }
}
