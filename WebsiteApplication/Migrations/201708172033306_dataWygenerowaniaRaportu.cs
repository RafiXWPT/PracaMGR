using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class dataWygenerowaniaRaportu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeneratedReaport", "GeneratedAt", c => c.DateTime(false));
        }

        public override void Down()
        {
            DropColumn("dbo.GeneratedReaport", "GeneratedAt");
        }
    }
}