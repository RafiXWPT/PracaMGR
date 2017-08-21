namespace PersonInfoService.Host.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondToLastName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Person", "LastName", c => c.String());
            DropColumn("dbo.Person", "SecondName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Person", "SecondName", c => c.String());
            DropColumn("dbo.Person", "LastName");
        }
    }
}
