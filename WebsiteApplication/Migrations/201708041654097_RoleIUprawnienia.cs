namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleIUprawnienia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Right",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RightName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RoleToRight",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RightId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RoleToRight");
            DropTable("dbo.Right");
        }
    }
}
