namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig11 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BadWords",
                c => new
                    {
                        WordId = c.Int(nullable: false, identity: true),
                        Word = c.String(),
                    })
                .PrimaryKey(t => t.WordId);
            
            CreateTable(
                "dbo.InformalBlogModels",
                c => new
                    {
                        InformalBlogModelId = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Message = c.String(),
                        Title = c.String(),
                        FilePath = c.String(),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InformalBlogModelId)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InformalBlogModels", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.InformalBlogModels", new[] { "Id" });
            DropTable("dbo.InformalBlogModels");
            DropTable("dbo.BadWords");
        }
    }
}
