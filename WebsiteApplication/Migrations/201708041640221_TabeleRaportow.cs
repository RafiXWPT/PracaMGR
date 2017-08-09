using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class TabeleRaportow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.GeneratedReaport",
                    c => new
                    {
                        Id = c.Guid(false),
                        PatientPesel = c.String(),
                        Reaport = c.Binary(),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                    "dbo.ReaportRequest",
                    c => new
                    {
                        Id = c.Guid(false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(false),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                    "dbo.SearchHistory",
                    c => new
                    {
                        Id = c.Guid(false),
                        PatientPesel = c.String(),
                        SearchedAt = c.DateTime(false),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SearchHistory", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReaportRequest", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GeneratedReaport", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.SearchHistory", new[] {"ApplicationUser_Id"});
            DropIndex("dbo.ReaportRequest", new[] {"ApplicationUser_Id"});
            DropIndex("dbo.GeneratedReaport", new[] {"ApplicationUser_Id"});
            DropTable("dbo.SearchHistory");
            DropTable("dbo.ReaportRequest");
            DropTable("dbo.GeneratedReaport");
        }
    }
}