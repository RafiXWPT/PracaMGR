namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dataWygenerowaniaRaportu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeneratedReaport", "GeneratedAt", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GeneratedReaport", "GeneratedAt");
        }
    }
}
