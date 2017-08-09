using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.CodeBehind.Attributes
{
    public class AuthorizeRightAttribute : AuthorizeAttribute
    {
        public string Right { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var resolver = DependencyResolver.Current;
            var userManager = resolver.GetService<ApplicationUserManager>();
            var rightsManager = resolver.GetService<IRightsManager<RightViewModel, RoleViewModel, UserViewModel>>();
            var user = userManager.Users.FirstOrDefault(u => u.UserName == httpContext.User.Identity.Name);
            if (user == null)
                return false;

            var neededRoles = rightsManager.RolesForRight(Right);
            var userRoles = rightsManager.RoleNamesForGuid(user.Roles.Select(r => r.RoleId).ToList());
            return userRoles.Any(userRole => neededRoles.Contains(userRole));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/NotAuthorize/Index");
        }
    }
}