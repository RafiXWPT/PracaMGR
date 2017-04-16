using System.Data.Entity.Migrations;

namespace PersonInfoService.Host.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Address",
                    c => new
                    {
                        AddressId = c.Guid(false),
                        Country = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        ZipCode = c.String(),
                        HomeNumber = c.String()
                    })
                .PrimaryKey(t => t.AddressId);

            CreateTable(
                    "dbo.Person",
                    c => new
                    {
                        PersonId = c.Guid(false),
                        Pesel = c.String(),
                        FirstName = c.String(),
                        SecondName = c.String(),
                        BirthDate = c.DateTime(false),
                        InsuranceId = c.String(),
                        Address_AddressId = c.Guid()
                    })
                .PrimaryKey(t => t.PersonId)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.Person", "Address_AddressId", "dbo.Address");
            DropIndex("dbo.Person", new[] {"Address_AddressId"});
            DropTable("dbo.Person");
            DropTable("dbo.Address");
        }
    }
}