using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Rights;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class IdentityController : Controller
    {
        private ApplicationPrincipal _identityPrincipal;

        public new ApplicationPrincipal User => _identityPrincipal ?? (_identityPrincipal = new ApplicationPrincipal(base.User as ClaimsPrincipal));
    }
}