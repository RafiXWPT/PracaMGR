using System.Data.Entity.Migrations;

namespace WebsiteApplication.Migrations
{
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.GeneratedReaport",
                    c => new
                    {
                        GeneratedReaportId = c.Guid(false),
                        PatientPesel = c.String(),
                        Reaport = c.Binary(),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.GeneratedReaportId)
                .ForeignKey("dbo.ReaportRequest", t => t.GeneratedReaportId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.GeneratedReaportId)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                    "dbo.ReaportRequest",
                    c => new
                    {
                        ReaportRequestId = c.Guid(false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(false),
                        CreatedBy = c.String(),
                        Status = c.Int(false),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.ReaportRequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                    "dbo.Institution",
                    c => new
                    {
                        InstitutionId = c.Guid(false),
                        InstitutionEndpointAddress = c.String(),
                        InstitutionName = c.String(),
                        Address_AddressId = c.Guid()
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);

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
                    "dbo.Right",
                    c => new
                    {
                        RightId = c.Guid(false),
                        RightName = c.String(),
                        RightDescription = c.String()
                    })
                .PrimaryKey(t => t.RightId);

            CreateTable(
                    "dbo.AspNetRoles",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Name = c.String(false, 256)
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");

            CreateTable(
                    "dbo.AspNetUserRoles",
                    c => new
                    {
                        UserId = c.String(false, 128),
                        RoleId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.UserId, t.RoleId})
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);

            CreateTable(
                    "dbo.RoleToRight",
                    c => new
                    {
                        RoleToRightId = c.Guid(false),
                        Role = c.String(),
                        RightId = c.Guid(false)
                    })
                .PrimaryKey(t => t.RoleToRightId)
                .ForeignKey("dbo.Right", t => t.RightId, true)
                .Index(t => t.RightId);

            CreateTable(
                    "dbo.SearchHistory",
                    c => new
                    {
                        SearchHistoryId = c.Guid(false),
                        PatientPesel = c.String(),
                        SearchedAt = c.DateTime(false),
                        SearchedBy = c.String(),
                        SearchType = c.Int(false),
                        ApplicationUser_Id = c.String(maxLength: 128)
                    })
                .PrimaryKey(t => t.SearchHistoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);

            CreateTable(
                    "dbo.AspNetUsers",
                    c => new
                    {
                        Id = c.String(false, 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(false),
                        TwoFactorEnabled = c.Boolean(false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(false),
                        AccessFailedCount = c.Int(false),
                        UserName = c.String(false, 256),
                        Address_AddressId = c.Guid()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Address_AddressId);

            CreateTable(
                    "dbo.AspNetUserClaims",
                    c => new
                    {
                        Id = c.Int(false, true),
                        UserId = c.String(false, 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String()
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);

            CreateTable(
                    "dbo.AspNetUserLogins",
                    c => new
                    {
                        LoginProvider = c.String(false, 128),
                        ProviderKey = c.String(false, 128),
                        UserId = c.String(false, 128)
                    })
                .PrimaryKey(t => new {t.LoginProvider, t.ProviderKey, t.UserId})
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, true)
                .Index(t => t.UserId);
        }

        public override void Down()
        {
            DropForeignKey("dbo.SearchHistory", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ReaportRequest", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GeneratedReaport", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Address_AddressId", "dbo.Address");
            DropForeignKey("dbo.RoleToRight", "RightId", "dbo.Right");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Institution", "Address_AddressId", "dbo.Address");
            DropForeignKey("dbo.GeneratedReaport", "GeneratedReaportId", "dbo.ReaportRequest");
            DropIndex("dbo.AspNetUserLogins", new[] {"UserId"});
            DropIndex("dbo.AspNetUserClaims", new[] {"UserId"});
            DropIndex("dbo.AspNetUsers", new[] {"Address_AddressId"});
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SearchHistory", new[] {"ApplicationUser_Id"});
            DropIndex("dbo.RoleToRight", new[] {"RightId"});
            DropIndex("dbo.AspNetUserRoles", new[] {"RoleId"});
            DropIndex("dbo.AspNetUserRoles", new[] {"UserId"});
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Institution", new[] {"Address_AddressId"});
            DropIndex("dbo.ReaportRequest", new[] {"ApplicationUser_Id"});
            DropIndex("dbo.GeneratedReaport", new[] {"ApplicationUser_Id"});
            DropIndex("dbo.GeneratedReaport", new[] {"GeneratedReaportId"});
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.SearchHistory");
            DropTable("dbo.RoleToRight");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Right");
            DropTable("dbo.Address");
            DropTable("dbo.Institution");
            DropTable("dbo.ReaportRequest");
            DropTable("dbo.GeneratedReaport");
        }
    }
}