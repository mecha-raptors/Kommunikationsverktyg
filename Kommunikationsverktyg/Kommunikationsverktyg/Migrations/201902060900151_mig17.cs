namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropIndex("dbo.AspNetUsers", new[] { "RequestedEventModel_EventId" });
            CreateTable(
                "dbo.RequestedEventModelApplicationUsers",
                c => new
                    {
                        RequestedEventModel_EventId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RequestedEventModel_EventId, t.ApplicationUser_Id })
                .ForeignKey("dbo.RequestedEventModels", t => t.RequestedEventModel_EventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.RequestedEventModel_EventId)
                .Index(t => t.ApplicationUser_Id);
            
            DropColumn("dbo.AspNetUsers", "RequestedEventModel_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "RequestedEventModel_EventId", c => c.Int());
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "RequestedEventModel_EventId" });
            DropTable("dbo.RequestedEventModelApplicationUsers");
            CreateIndex("dbo.AspNetUsers", "RequestedEventModel_EventId");
            AddForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId");
        }
    }
}
