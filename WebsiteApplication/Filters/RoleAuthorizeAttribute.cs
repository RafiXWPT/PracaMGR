using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace WebsiteApplication.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var basicAuthorize = base.AuthorizeCore(httpContext);
            return basicAuthorize && IsInAnyRole(httpContext.User, Roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/NotAuthorize/");
        }

        private static bool IsInAnyRole(IPrincipal user, string roles)
        {
            return roles.Split(',').Any(user.IsInRole);
        }
    }
}