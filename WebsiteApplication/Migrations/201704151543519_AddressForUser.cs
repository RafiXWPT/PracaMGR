namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddressForUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Address_AddressId", c => c.Guid());
            CreateIndex("dbo.AspNetUsers", "Address_AddressId");
            AddForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Address", "AddressId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Address");
            DropIndex("dbo.AspNetUsers", new[] { "Address_AddressId" });
            DropColumn("dbo.AspNetUsers", "Address_AddressId");
        }
    }
}
