using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using WebsiteApplication;
using WebsiteApplication.CodeBehind.Report;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models;
using WebsiteApplication.Models.ViewModels.Rights;

[assembly: OwinStartup(typeof(Startup))]

namespace WebsiteApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.RegisterSingleton(app);
            container.Register(() => new WebsiteDatabaseContext("WebsiteDatabase"), Lifestyle.Scoped);
            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);
            container.Register<IRepository<Institution>, InstitutionRepository>(Lifestyle.Scoped);
            container.Register<IRepository<ReportRequest>, ReportRequestRepository>(Lifestyle.Scoped);
            container.Register<IRepository<GeneratedReport>, GeneratedReportRepository>(Lifestyle.Scoped);
            container.Register<IRepository<SearchHistory>, SearchHistoryRepository>(Lifestyle.Scoped);
            container.Register<IRightsManager<RightViewModel, RoleViewModel, UserViewModel>, RightsManager>(Lifestyle.Scoped);
            container.Register<IReportService, PdfReportService>(Lifestyle.Scoped);

            container.Register<IUserStore<ApplicationUser>>(
                () => new UserStore<ApplicationUser>(container.GetInstance<WebsiteDatabaseContext>()),
                Lifestyle.Scoped);

            container.Register(
                () => container.IsVerifying
                    ? new OwinContext(new Dictionary<string, object>()).Authentication
                    : HttpContext.Current.GetOwinContext().Authentication, Lifestyle.Scoped);

            container.RegisterInitializer<ApplicationUserManager>(manager => InitializeUserManager(manager, app));

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            ConfigureAuth(app, container);
        }

        private static void InitializeUserManager(ApplicationUserManager manager, IAppBuilder app)
        {
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true
            };

            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            var dataProtectionProvider = app.GetDataProtectionProvider();
            if (dataProtectionProvider != null)
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }
    }
}