using System.Web.Mvc;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class MenuController : IdentityController
    {
        public PartialViewResult Menu()
        {
            return PartialView("_KendoHorizontal");
        }
    }
}