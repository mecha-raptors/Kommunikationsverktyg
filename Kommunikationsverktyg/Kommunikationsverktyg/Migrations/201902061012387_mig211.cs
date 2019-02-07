namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig211 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RequestedEventModels", new[] { "EventInvitedUsers_InviteId" });
            RenameColumn(table: "dbo.EventInvitedUsers", name: "EventInvitedUsers_InviteId", newName: "Event_EventId");
            CreateIndex("dbo.EventInvitedUsers", "Event_EventId");
            DropColumn("dbo.RequestedEventModels", "EventInvitedUsers_InviteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RequestedEventModels", "EventInvitedUsers_InviteId", c => c.Int());
            DropIndex("dbo.EventInvitedUsers", new[] { "Event_EventId" });
            RenameColumn(table: "dbo.EventInvitedUsers", name: "Event_EventId", newName: "EventInvitedUsers_InviteId");
            CreateIndex("dbo.RequestedEventModels", "EventInvitedUsers_InviteId");
        }
    }
}
