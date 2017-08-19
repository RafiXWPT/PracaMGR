namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RejectedAcceptedAtBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReaportRequest", "AcceptedAt", c => c.DateTime());
            AddColumn("dbo.ReaportRequest", "AcceptedBy", c => c.String());
            AddColumn("dbo.ReaportRequest", "RejectedAt", c => c.DateTime());
            AddColumn("dbo.ReaportRequest", "RejectedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReaportRequest", "RejectedBy");
            DropColumn("dbo.ReaportRequest", "RejectedAt");
            DropColumn("dbo.ReaportRequest", "AcceptedBy");
            DropColumn("dbo.ReaportRequest", "AcceptedAt");
        }
    }
}
