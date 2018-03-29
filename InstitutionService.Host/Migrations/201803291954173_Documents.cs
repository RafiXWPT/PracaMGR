namespace InstitutionService.Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Documents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HospitalizationDocument",
                c => new
                    {
                        HospitalizationDocumentId = c.Guid(nullable: false),
                        Name = c.String(),
                        Content = c.Binary(),
                        HospitalizationId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.HospitalizationDocumentId)
                .ForeignKey("dbo.Hospitalization", t => t.HospitalizationId, cascadeDelete: true)
                .Index(t => t.HospitalizationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HospitalizationDocument", "HospitalizationId", "dbo.Hospitalization");
            DropIndex("dbo.HospitalizationDocument", new[] { "HospitalizationId" });
            DropTable("dbo.HospitalizationDocument");
        }
    }
}
