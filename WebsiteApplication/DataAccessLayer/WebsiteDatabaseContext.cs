using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.Models;

namespace WebsiteApplication.DataAccessLayer
{
    class WebsiteDatabaseContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Institution> Institutions { get; set; }
        public WebsiteDatabaseContext() : base("WebsiteContext", false) { }
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
