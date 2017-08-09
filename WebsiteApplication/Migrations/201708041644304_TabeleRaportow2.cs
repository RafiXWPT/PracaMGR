using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class TabeleRaportow2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReaportRequest", "Status", c => c.Int(false));
            AddColumn("dbo.ReaportRequest", "Comment", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ReaportRequest", "Comment");
            DropColumn("dbo.ReaportRequest", "Status");
        }
    }
}