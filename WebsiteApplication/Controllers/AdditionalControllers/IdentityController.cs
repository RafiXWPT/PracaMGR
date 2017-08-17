using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Classess;

namespace WebsiteApplication.Controllers
{
    public class IdentityController : Controller
    {
        private ApplicationPrincipal _identityPrincipal;

        public new ApplicationPrincipal User => _identityPrincipal ?? (_identityPrincipal = new ApplicationPrincipal(base.User as ClaimsPrincipal));
    }
}