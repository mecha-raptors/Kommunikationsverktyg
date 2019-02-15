namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class skjutMig : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EventModels", new[] { "EventCreator_Id" });
            AlterColumn("dbo.EventModels", "EventCreator_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.EventModels", "EventCreator_Id");
            AddForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers");
            DropIndex("dbo.EventModels", new[] { "EventCreator_Id" });
            AlterColumn("dbo.EventModels", "EventCreator_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.EventModels", "EventCreator_Id");
            AddForeignKey("dbo.EventModels", "EventCreator_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
