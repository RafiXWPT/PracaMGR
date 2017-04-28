using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class NazwaInstytucji : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Institution", "InstitutionName", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Institution", "InstitutionName");
        }
    }
}