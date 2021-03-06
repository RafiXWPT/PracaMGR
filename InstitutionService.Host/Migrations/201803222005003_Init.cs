namespace InstitutionService.Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Examination",
                c => new
                    {
                        ExaminationId = c.Guid(nullable: false),
                        HospitalizationId = c.Guid(nullable: false),
                        ExaminationStartTime = c.DateTime(nullable: false),
                        ExaminationEndTime = c.DateTime(nullable: false),
                        ExaminationDetails = c.String(),
                    })
                .PrimaryKey(t => t.ExaminationId)
                .ForeignKey("dbo.Hospitalization", t => t.HospitalizationId, cascadeDelete: true)
                .Index(t => t.HospitalizationId);
            
            CreateTable(
                "dbo.Hospitalization",
                c => new
                    {
                        HospitalizationId = c.Guid(nullable: false),
                        PatientId = c.Guid(nullable: false),
                        HospitalizationStartTime = c.DateTime(nullable: false),
                        HospitalizationEndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.HospitalizationId)
                .ForeignKey("dbo.Patient", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        PatientId = c.Guid(nullable: false),
                        Pesel = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.Treatment",
                c => new
                    {
                        TreatmentId = c.Guid(nullable: false),
                        HospitalizationId = c.Guid(nullable: false),
                        TreatmentStartDate = c.DateTime(nullable: false),
                        TreatmentEndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TreatmentId)
                .ForeignKey("dbo.Hospitalization", t => t.HospitalizationId, cascadeDelete: true)
                .Index(t => t.HospitalizationId);
            
            CreateTable(
                "dbo.UsedMedicine",
                c => new
                    {
                        UsedMedicineId = c.Guid(nullable: false),
                        TreatmentId = c.Guid(nullable: false),
                        MedicineId = c.Guid(nullable: false),
                        Dose = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.UsedMedicineId)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, cascadeDelete: true)
                .ForeignKey("dbo.Treatment", t => t.TreatmentId, cascadeDelete: true)
                .Index(t => t.TreatmentId)
                .Index(t => t.MedicineId);
            
            CreateTable(
                "dbo.Medicine",
                c => new
                    {
                        MedicineId = c.Guid(nullable: false),
                        MedicineName = c.String(),
                    })
                .PrimaryKey(t => t.MedicineId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsedMedicine", "TreatmentId", "dbo.Treatment");
            DropForeignKey("dbo.UsedMedicine", "MedicineId", "dbo.Medicine");
            DropForeignKey("dbo.Treatment", "HospitalizationId", "dbo.Hospitalization");
            DropForeignKey("dbo.Hospitalization", "PatientId", "dbo.Patient");
            DropForeignKey("dbo.Examination", "HospitalizationId", "dbo.Hospitalization");
            DropIndex("dbo.UsedMedicine", new[] { "MedicineId" });
            DropIndex("dbo.UsedMedicine", new[] { "TreatmentId" });
            DropIndex("dbo.Treatment", new[] { "HospitalizationId" });
            DropIndex("dbo.Hospitalization", new[] { "PatientId" });
            DropIndex("dbo.Examination", new[] { "HospitalizationId" });
            DropTable("dbo.Medicine");
            DropTable("dbo.UsedMedicine");
            DropTable("dbo.Treatment");
            DropTable("dbo.Patient");
            DropTable("dbo.Hospitalization");
            DropTable("dbo.Examination");
        }
    }
}
