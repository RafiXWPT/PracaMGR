namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReportInsteadReaport : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GeneratedReaport", "GeneratedReaportId", "dbo.ReaportRequest");
            DropIndex("dbo.GeneratedReaport", new[] { "GeneratedReaportId" });
            CreateTable(
                "dbo.GeneratedReport",
                c => new
                    {
                        GeneratedReportId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Report = c.Binary(),
                    })
                .PrimaryKey(t => t.GeneratedReportId)
                .ForeignKey("dbo.ReportRequest", t => t.GeneratedReportId)
                .Index(t => t.GeneratedReportId);
            
            CreateTable(
                "dbo.ReportRequest",
                c => new
                    {
                        ReportRequestId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        PatientFirstName = c.String(),
                        PatientLastName = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        AcceptedAt = c.DateTime(),
                        AcceptedBy = c.String(),
                        RejectedAt = c.DateTime(),
                        RejectedBy = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReportRequestId);
            
            DropTable("dbo.GeneratedReaport");
            DropTable("dbo.ReaportRequest");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReaportRequest",
                c => new
                    {
                        ReaportRequestId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        AcceptedAt = c.DateTime(),
                        AcceptedBy = c.String(),
                        RejectedAt = c.DateTime(),
                        RejectedBy = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ReaportRequestId);
            
            CreateTable(
                "dbo.GeneratedReaport",
                c => new
                    {
                        GeneratedReaportId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Reaport = c.Binary(),
                    })
                .PrimaryKey(t => t.GeneratedReaportId);
            
            DropForeignKey("dbo.GeneratedReport", "GeneratedReportId", "dbo.ReportRequest");
            DropIndex("dbo.GeneratedReport", new[] { "GeneratedReportId" });
            DropTable("dbo.ReportRequest");
            DropTable("dbo.GeneratedReport");
            CreateIndex("dbo.GeneratedReaport", "GeneratedReaportId");
            AddForeignKey("dbo.GeneratedReaport", "GeneratedReaportId", "dbo.ReaportRequest", "ReaportRequestId");
        }
    }
}
