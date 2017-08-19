using System;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Extensions;

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