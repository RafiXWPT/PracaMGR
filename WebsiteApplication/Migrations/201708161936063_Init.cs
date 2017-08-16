namespace WebsiteApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GeneratedReaport",
                c => new
                    {
                        GeneratedReaportId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        Reaport = c.Binary(),
                        ApplicationUser_Id = c.String(maxLength: 128),
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
                        ReaportRequestId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        Status = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ReaportRequestId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Institution",
                c => new
                    {
                        InstitutionId = c.Guid(nullable: false),
                        InstitutionEndpointAddress = c.String(),
                        InstitutionName = c.String(),
                        Address_AddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.InstitutionId)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        AddressId = c.Guid(nullable: false),
                        Country = c.String(),
                        Province = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        ZipCode = c.String(),
                        HomeNumber = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Right",
                c => new
                    {
                        RightId = c.Guid(nullable: false),
                        RightName = c.String(),
                        RightDescription = c.String(),
                    })
                .PrimaryKey(t => t.RightId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.RoleToRight",
                c => new
                    {
                        RoleToRightId = c.Guid(nullable: false),
                        Role = c.String(),
                        RightId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.RoleToRightId)
                .ForeignKey("dbo.Right", t => t.RightId, cascadeDelete: true)
                .Index(t => t.RightId);
            
            CreateTable(
                "dbo.SearchHistory",
                c => new
                    {
                        SearchHistoryId = c.Guid(nullable: false),
                        PatientPesel = c.String(),
                        SearchedAt = c.DateTime(nullable: false),
                        SearchedBy = c.String(),
                        SearchType = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SearchHistoryId)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Address_AddressId = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.Address_AddressId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Address_AddressId);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
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
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Address_AddressId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SearchHistory", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.RoleToRight", new[] { "RightId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Institution", new[] { "Address_AddressId" });
            DropIndex("dbo.ReaportRequest", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GeneratedReaport", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GeneratedReaport", new[] { "GeneratedReaportId" });
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
