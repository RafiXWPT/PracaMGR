using System;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind;

namespace WebsiteApplication.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RoleAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var basicAuthorize = base.AuthorizeCore(httpContext);
            return basicAuthorize && httpContext.User.IsInAnyRole(Roles);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/NotAuthorize/");
        }
    }
}