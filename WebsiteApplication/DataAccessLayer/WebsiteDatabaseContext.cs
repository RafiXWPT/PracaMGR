using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.Models;

namespace WebsiteApplication.DataAccessLayer
{
    public class WebsiteDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Institution> Institutions { get; set; }
        public WebsiteDatabaseContext() : base("WebsiteDatabase", false) { }
        public WebsiteDatabaseContext(string connectionName) : base(connectionName, false) { }

        static WebsiteDatabaseContext()
        {
            Database.SetInitializer<WebsiteDatabaseContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
