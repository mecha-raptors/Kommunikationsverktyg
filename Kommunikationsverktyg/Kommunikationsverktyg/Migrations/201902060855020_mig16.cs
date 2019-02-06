namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig16 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RequestedEventModel_EventId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "RequestedEventModel_EventId");
            AddForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels", "EventId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropIndex("dbo.AspNetUsers", new[] { "RequestedEventModel_EventId" });
            DropColumn("dbo.AspNetUsers", "RequestedEventModel_EventId");
        }
    }
}
