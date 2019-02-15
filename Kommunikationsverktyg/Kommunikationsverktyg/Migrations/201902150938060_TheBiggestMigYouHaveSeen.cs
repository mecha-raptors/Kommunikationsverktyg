namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TheBiggestMigYouHaveSeen : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropForeignKey("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "RequestedEventModel_EventId" });
            DropIndex("dbo.RequestedEventModelApplicationUsers", new[] { "ApplicationUser_Id" });
            RenameColumn(table: "dbo.DateModels", name: "RequestedEventModel_EventId", newName: "EventConnection_EventId");
            RenameIndex(table: "dbo.DateModels", name: "IX_RequestedEventModel_EventId", newName: "IX_EventConnection_EventId");
            AddColumn("dbo.AspNetUsers", "RequestedEventModel_EventId", c => c.Int());
            AddColumn("dbo.RequestedEventModels", "EventCreator_Id", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.RequestedEventModels", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.EventModels", "Location", c => c.String());
            AddColumn("dbo.EventModels", "EventCreator_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "RequestedEventModel_EventId");
            CreateIndex("dbo.RequestedEventModels", "EventCreator_Id");
            CreateIndex("dbo.RequestedEventModels", "ApplicationUser_Id");
            CreateIndex("dbo.EventModels", "EventCreator_Id");
            AddForeignKey("dbo.RequestedEventModels", "EventCreator_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId");
            AddForeignKey("dbo.RequestedEventModels", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
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
            
            DropForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.RequestedEventModels", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropForeignKey("dbo.RequestedEventModels", "EventCreator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EventModels", new[] { "EventCreator_Id" });
            DropIndex("dbo.RequestedEventModels", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RequestedEventModels", new[] { "EventCreator_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "RequestedEventModel_EventId" });
            DropColumn("dbo.EventModels", "EventCreator_Id");
            DropColumn("dbo.EventModels", "Location");
            DropColumn("dbo.RequestedEventModels", "ApplicationUser_Id");
            DropColumn("dbo.RequestedEventModels", "EventCreator_Id");
            DropColumn("dbo.AspNetUsers", "RequestedEventModel_EventId");
            RenameIndex(table: "dbo.DateModels", name: "IX_EventConnection_EventId", newName: "IX_RequestedEventModel_EventId");
            RenameColumn(table: "dbo.DateModels", name: "EventConnection_EventId", newName: "RequestedEventModel_EventId");
            CreateIndex("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId");
            AddForeignKey("dbo.RequestedEventModelApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RequestedEventModelApplicationUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId", cascadeDelete: true);
        }
    }
}
