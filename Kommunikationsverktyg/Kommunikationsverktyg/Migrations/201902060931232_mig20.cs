namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "RequestedEventModel_EventId" });
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "ApplicationUser_Id" });
            DropTable("dbo.RequestedEventModelApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RequestedEventModelApplicationUsers",
                c => new
                    {
                        RequestedEventModel_EventId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RequestedEventModel_EventId, t.ApplicationUser_Id });
            
            CreateIndex("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId");
            AddForeignKey("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId", cascadeDelete: true);
        }
    }
}
