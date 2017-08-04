namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rights3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Right", "RightDescription", c => c.String());
            AddColumn("dbo.RoleToRight", "Role", c => c.String());
            DropColumn("dbo.RoleToRight", "RoleId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoleToRight", "RoleId", c => c.Guid(nullable: false));
            DropColumn("dbo.RoleToRight", "Role");
            DropColumn("dbo.Right", "RightDescription");
        }
    }
}
