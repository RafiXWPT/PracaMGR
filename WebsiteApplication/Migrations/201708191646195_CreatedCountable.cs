namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedCountable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GeneratedReaport", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.SearchHistory", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.SearchHistory", "CreatedBy", c => c.String());
            DropColumn("dbo.GeneratedReaport", "GeneratedAt");
            DropColumn("dbo.SearchHistory", "SearchedAt");
            DropColumn("dbo.SearchHistory", "SearchedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchHistory", "SearchedBy", c => c.String());
            AddColumn("dbo.SearchHistory", "SearchedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.GeneratedReaport", "GeneratedAt", c => c.DateTime(nullable: false));
            DropColumn("dbo.SearchHistory", "CreatedBy");
            DropColumn("dbo.SearchHistory", "CreatedAt");
            DropColumn("dbo.GeneratedReaport", "CreatedAt");
        }
    }
}
