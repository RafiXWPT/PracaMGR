using System.Web.Mvc;

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