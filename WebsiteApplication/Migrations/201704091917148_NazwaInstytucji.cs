namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
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
