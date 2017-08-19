using System.Security.Claims;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Classess;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class IdentityController : Controller
    {
        private ApplicationPrincipal _identityPrincipal;

        public new ApplicationPrincipal User {
            get
            {
                if (_identityPrincipal != null)
                    return _identityPrincipal;

                if (!(base.User is ClaimsPrincipal))
                    _identityPrincipal = new ApplicationPrincipal();
                else
                    _identityPrincipal = new ApplicationPrincipal((ClaimsPrincipal) base.User);

                return _identityPrincipal;
            }
        }
    }
}