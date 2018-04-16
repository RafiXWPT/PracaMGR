namespace InstitutionService.Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rozbudowa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Examination", "Examinator", c => c.String());
            AddColumn("dbo.Examination", "UsedDevices", c => c.String());
            AddColumn("dbo.Examination", "SignedReceip", c => c.Boolean(nullable: false));
            AddColumn("dbo.Examination", "SickLeave", c => c.Boolean(nullable: false));
            AddColumn("dbo.Examination", "SickLeaveFrom", c => c.DateTime());
            AddColumn("dbo.Examination", "SickLeaveTo", c => c.DateTime());
            AddColumn("dbo.Examination", "PrivateVisit", c => c.Boolean(nullable: false));
            AddColumn("dbo.Treatment", "Personel", c => c.String());
            AddColumn("dbo.Treatment", "UsedDevices", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Treatment", "UsedDevices");
            DropColumn("dbo.Treatment", "Personel");
            DropColumn("dbo.Examination", "PrivateVisit");
            DropColumn("dbo.Examination", "SickLeaveTo");
            DropColumn("dbo.Examination", "SickLeaveFrom");
            DropColumn("dbo.Examination", "SickLeave");
            DropColumn("dbo.Examination", "SignedReceip");
            DropColumn("dbo.Examination", "UsedDevices");
            DropColumn("dbo.Examination", "Examinator");
        }
    }
}
