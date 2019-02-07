namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlacardModels", "Fullname", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlacardModels", "Fullname");
        }
    }
}
