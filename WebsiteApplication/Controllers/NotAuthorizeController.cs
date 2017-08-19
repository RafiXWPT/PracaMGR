using System.Web.Mvc;
using WebsiteApplication.Controllers.AdditionalControllers;

namespace WebsiteApplication.Controllers
{
    public class NotAuthorizeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}