namespace Kommunikationsverktyg.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Anslagstest : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlacardModels", "TypeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PlacardModels", "TypeName");
        }
    }
}
