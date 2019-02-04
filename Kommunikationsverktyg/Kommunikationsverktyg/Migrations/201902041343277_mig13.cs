namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "EventModel_EventId", "dbo.EventModels");
            DropIndex("dbo.AspNetUsers", new[] { "EventModel_EventId" });
            DropColumn("dbo.AspNetUsers", "EventModel_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "EventModel_EventId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "EventModel_EventId");
            AddForeignKey("dbo.AspNetUsers", "EventModel_EventId", "dbo.EventModels", "EventId");
        }
    }
}
