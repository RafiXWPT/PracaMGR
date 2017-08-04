namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RoleIUprawnienia2 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.RoleToRight", "RightId");
            AddForeignKey("dbo.RoleToRight", "RightId", "dbo.Right", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoleToRight", "RightId", "dbo.Right");
            DropIndex("dbo.RoleToRight", new[] { "RightId" });
        }
    }
}
