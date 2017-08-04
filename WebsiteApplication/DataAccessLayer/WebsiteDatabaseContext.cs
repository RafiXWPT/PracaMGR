using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.Models;

namespace WebsiteApplication.DataAccessLayer
{
    public class WebsiteDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        /* USER PASSWORDS => P@ssword123 */
        static WebsiteDatabaseContext()
        {
            Database.SetInitializer<WebsiteDatabaseContext>(null);
        }

        public WebsiteDatabaseContext() : base("WebsiteDatabase", false)
        {
        }

        public WebsiteDatabaseContext(string connectionName) : base(connectionName, false)
        {
        }

        public DbSet<Institution> Institutions { get; set; }
        public DbSet<GeneratedReaport> GeneratedReaports { get; set; }
        public DbSet<ReaportRequest> ReaportRequests { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<RoleToRight> RolesToRights { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}