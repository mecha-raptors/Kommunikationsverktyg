namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RequestedEventModels",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.DateModels",
                c => new
                    {
                        DateId = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        RequestedEventModel_EventId = c.Int(),
                    })
                .PrimaryKey(t => t.DateId)
                .ForeignKey("dbo.RequestedEventModels", t => t.RequestedEventModel_EventId)
                .Index(t => t.RequestedEventModel_EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DateModels", "RequestedEventModel_EventId", "dbo.RequestedEventModels");
            DropIndex("dbo.DateModels", new[] { "RequestedEventModel_EventId" });
            DropTable("dbo.DateModels");
            DropTable("dbo.RequestedEventModels");
        }
    }
}
