using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class Rights3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Right", "RightDescription", c => c.String());
            AddColumn("dbo.RoleToRight", "Role", c => c.String());
            DropColumn("dbo.RoleToRight", "RoleId");
        }

        public override void Down()
        {
            AddColumn("dbo.RoleToRight", "RoleId", c => c.Guid(false));
            DropColumn("dbo.RoleToRight", "Role");
            DropColumn("dbo.Right", "RightDescription");
        }
    }
}