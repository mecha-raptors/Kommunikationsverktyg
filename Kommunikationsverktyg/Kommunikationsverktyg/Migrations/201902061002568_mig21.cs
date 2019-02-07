namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig21 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventInvitedUsers",
                c => new
                    {
                        InviteId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.InviteId);
            
            AddColumn("dbo.AspNetUsers", "EventInvitedUsers_InviteId", c => c.Int());
            AddColumn("dbo.RequestedEventModels", "EventInvitedUsers_InviteId", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "EventInvitedUsers_InviteId");
            CreateIndex("dbo.RequestedEventModels", "EventInvitedUsers_InviteId");
            AddForeignKey("dbo.RequestedEventModels", "EventInvitedUsers_InviteId", "dbo.EventInvitedUsers", "InviteId");
            AddForeignKey("dbo.AspNetUsers", "EventInvitedUsers_InviteId", "dbo.EventInvitedUsers", "InviteId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "EventInvitedUsers_InviteId", "dbo.EventInvitedUsers");
            DropForeignKey("dbo.RequestedEventModels", "EventInvitedUsers_InviteId", "dbo.EventInvitedUsers");
            DropIndex("dbo.RequestedEventModels", new[] { "EventInvitedUsers_InviteId" });
            DropIndex("dbo.AspNetUsers", new[] { "EventInvitedUsers_InviteId" });
            DropColumn("dbo.RequestedEventModels", "EventInvitedUsers_InviteId");
            DropColumn("dbo.AspNetUsers", "EventInvitedUsers_InviteId");
            DropTable("dbo.EventInvitedUsers");
        }
    }
}
