namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HistoriaWyszukiwania : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SearchHistory", "HospitalizationId", c => c.Guid());
            AddColumn("dbo.SearchHistory", "ExaminationId", c => c.Guid());
            AddColumn("dbo.SearchHistory", "TreatmentId", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SearchHistory", "TreatmentId");
            DropColumn("dbo.SearchHistory", "ExaminationId");
            DropColumn("dbo.SearchHistory", "HospitalizationId");
        }
    }
}
