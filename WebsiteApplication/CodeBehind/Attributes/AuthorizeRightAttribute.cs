using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.Models;
using WebsiteApplication.Models.ViewModels.Rights;

namespace WebsiteApplication.CodeBehind.Attributes
{
    public class AuthorizeRightAttribute : AuthorizeAttribute
    {
        public string Right { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return new ApplicationPrincipal(ClaimsPrincipal.Current).Rights.Any(r => r == Right);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/NotAuthorize/Index");
        }
    }
}