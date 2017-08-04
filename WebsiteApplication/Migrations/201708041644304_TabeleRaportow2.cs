namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TabeleRaportow2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReaportRequest", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.ReaportRequest", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReaportRequest", "Comment");
            DropColumn("dbo.ReaportRequest", "Status");
        }
    }
}
