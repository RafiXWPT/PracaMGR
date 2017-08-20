using System.Web.Mvc;
using WebsiteApplication.Models.ViewModels.Menu;

namespace WebsiteApplication.Controllers.AdditionalControllers
{
    public class MenuController : IdentityController
    {
        public PartialViewResult Menu()
        {
            return PartialView("_KendoHorizontal",
                new UserInfoContainerViewModel {Rights = User.Rights, Roles = User.Roles});
        }
    }
}