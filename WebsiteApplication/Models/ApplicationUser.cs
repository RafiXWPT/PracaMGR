using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.DataAccessLayer;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.Models
{
    public static class ApplicationClaims
    {
        public static string Rights => "RIGHTS";
    }
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        private readonly IRightsManager<RightViewModel, RoleViewModel, UserViewModel> _manager;
        public virtual Address Address { get; set; }

        public ApplicationUser()
        {
            var dbContext = new WebsiteDatabaseContext();
            _manager = new RightsManager(dbContext, new ApplicationUserManager(new UserStore<ApplicationUser>(dbContext)));
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            var userFromIdentity = _manager.Users().FirstOrDefault(u => u.Name == userIdentity.Name);
            if (userFromIdentity != null)
            {
                var rights = new List<string>();
                foreach (var userRole in userFromIdentity.Roles.Select(r => r.Name))
                {
                    var rightsForRole = _manager.RightsForRole(userRole);
                    foreach (var right in rightsForRole)
                    {
                        if (!rights.Contains(right))
                            rights.Add(right);
                    }
                }
                userIdentity.AddClaims(rights.Select(r => new Claim(ApplicationClaims.Rights, r)));
            }

            return userIdentity;
        }
    }
}