using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class RoleIUprawnienia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Right",
                    c => new
                    {
                        Id = c.Guid(false),
                        RightName = c.String()
                    })
                .PrimaryKey(t => t.Id);

            CreateTable(
                    "dbo.RoleToRight",
                    c => new
                    {
                        Id = c.Guid(false),
                        RightId = c.Guid(false),
                        RoleId = c.Guid(false)
                    })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.RoleToRight");
            DropTable("dbo.Right");
        }
    }
}