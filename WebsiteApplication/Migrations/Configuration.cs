using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models;

namespace WebsiteApplication.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WebsiteDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebsiteDatabaseContext context)
        {
            if (context.Users.FirstOrDefault(x => x.Email == "admin@admin.admin") != null)
                return;


            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            userManager.Create(new ApplicationUser {Email = "admin@admin.admin", UserName = "admin@admin.admin"},
                "P@ssword123");
            userManager.Create(new ApplicationUser {Email = "admin_tech@tech.tech", UserName = "admin_tech@tech.tech"},
                "P@ssword123");
            userManager.Create(new ApplicationUser {Email = "doctor@doctor.doctor", UserName = "doctor@doctor.doctor"},
                "P@ssword123");
            userManager.Create(new ApplicationUser {Email = "tech@tech.tech", UserName = "tech@tech.tech"},
                "P@ssword123");
            context.SaveChanges();

            var adminUser = context.Users.FirstOrDefault(x => x.UserName == "admin@admin.admin");
            userManager.AddToRole(adminUser.Id, "ADMIN");

            var adminTechUser = context.Users.FirstOrDefault(x => x.UserName == "admin_tech@tech.tech");
            userManager.AddToRole(adminTechUser.Id, "ADMIN_TECH");

            var doctorUser = context.Users.FirstOrDefault(x => x.UserName == "doctor@doctor.doctor");
            userManager.AddToRole(doctorUser.Id, "DOCTOR");

            var techUser = context.Users.FirstOrDefault(x => x.UserName == "tech@tech.tech");
            userManager.AddToRole(techUser.Id, "TECHNICAN");
            context.SaveChanges();


            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}