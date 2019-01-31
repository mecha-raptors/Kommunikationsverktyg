namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EventModel_EventId", c => c.Int());
            AddColumn("dbo.AspNetUsers", "RequestedEventModel_EventId", c => c.Int());
            AddColumn("dbo.DateModels", "Votes", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "EventModel_EventId");
            CreateIndex("dbo.AspNetUsers", "RequestedEventModel_EventId");
            AddForeignKey("dbo.AspNetUsers", "EventModel_EventId", "dbo.EventModels", "EventId");
            AddForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropForeignKey("dbo.AspNetUsers", "EventModel_EventId", "dbo.EventModels");
            DropIndex("dbo.AspNetUsers", new[] { "RequestedEventModel_EventId" });
            DropIndex("dbo.AspNetUsers", new[] { "EventModel_EventId" });
            DropColumn("dbo.DateModels", "Votes");
            DropColumn("dbo.AspNetUsers", "RequestedEventModel_EventId");
            DropColumn("dbo.AspNetUsers", "EventModel_EventId");
        }
    }
}
