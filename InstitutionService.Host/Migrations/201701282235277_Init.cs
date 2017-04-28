using System.Data.Entity.Migrations;

namespace InstitutionService.Host.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Examination",
                    c => new
                    {
                        ExaminationId = c.Guid(false),
                        HospitalizationId = c.Guid(false),
                        ExaminationStartTime = c.DateTime(false),
                        ExaminationEndTime = c.DateTime(false),
                        ExaminationDetails = c.String()
                    })
                .PrimaryKey(t => t.ExaminationId)
                .ForeignKey("dbo.Hospitalization", t => t.HospitalizationId, true)
                .Index(t => t.HospitalizationId);

            CreateTable(
                    "dbo.Hospitalization",
                    c => new
                    {
                        HospitalizationId = c.Guid(false),
                        PatientId = c.Guid(false),
                        HospitalizationStartTime = c.DateTime(false),
                        HospitalizationEndTime = c.DateTime(false)
                    })
                .PrimaryKey(t => t.HospitalizationId)
                .ForeignKey("dbo.Patient", t => t.PatientId, true)
                .Index(t => t.PatientId);

            CreateTable(
                    "dbo.Patient",
                    c => new
                    {
                        PatientId = c.Guid(false),
                        Pesel = c.String()
                    })
                .PrimaryKey(t => t.PatientId);

            CreateTable(
                    "dbo.Treatment",
                    c => new
                    {
                        TreatmentId = c.Guid(false),
                        HospitalizationId = c.Guid(false),
                        TreatmentDateTime = c.DateTime(false)
                    })
                .PrimaryKey(t => t.TreatmentId)
                .ForeignKey("dbo.Hospitalization", t => t.HospitalizationId, true)
                .Index(t => t.HospitalizationId);

            CreateTable(
                    "dbo.UsedMedicine",
                    c => new
                    {
                        UsedMedicineId = c.Guid(false),
                        TreatmentId = c.Guid(false),
                        MedicineId = c.Guid(false),
                        Dose = c.Double(false)
                    })
                .PrimaryKey(t => t.UsedMedicineId)
                .ForeignKey("dbo.Medicine", t => t.MedicineId, true)
                .ForeignKey("dbo.Treatment", t => t.TreatmentId, true)
                .Index(t => t.TreatmentId)
                .Index(t => t.MedicineId);

            CreateTable(
                    "dbo.Medicine",
                    c => new
                    {
                        MedicineId = c.Guid(false),
                        MedicineName = c.String()
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
            DropIndex("dbo.UsedMedicine", new[] {"MedicineId"});
            DropIndex("dbo.UsedMedicine", new[] {"TreatmentId"});
            DropIndex("dbo.Treatment", new[] {"HospitalizationId"});
            DropIndex("dbo.Hospitalization", new[] {"PatientId"});
            DropIndex("dbo.Examination", new[] {"HospitalizationId"});
            DropTable("dbo.Medicine");
            DropTable("dbo.UsedMedicine");
            DropTable("dbo.Treatment");
            DropTable("dbo.Patient");
            DropTable("dbo.Hospitalization");
            DropTable("dbo.Examination");
        }
    }
}