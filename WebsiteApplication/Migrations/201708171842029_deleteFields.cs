namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteFields : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GeneratedReaport", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReaportRequest", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SearchHistory", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GeneratedReaport", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ReaportRequest", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.SearchHistory", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.GeneratedReaport", "ApplicationUser_Id");
            DropColumn("dbo.ReaportRequest", "ApplicationUser_Id");
            DropColumn("dbo.SearchHistory", "ApplicationUser_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SearchHistory", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ReaportRequest", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.GeneratedReaport", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.SearchHistory", "ApplicationUser_Id");
            CreateIndex("dbo.ReaportRequest", "ApplicationUser_Id");
            CreateIndex("dbo.GeneratedReaport", "ApplicationUser_Id");
            AddForeignKey("dbo.SearchHistory", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ReaportRequest", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.GeneratedReaport", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
