using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind;
using WebsiteApplication.DataAccessLayer;

namespace WebsiteApplication.Controllers
{
    public class BaseController : Controller
    {
        public WebsiteDatabaseContext Context { get; }
        public ApplicationUserManager UserManager { get; }
        
        public BaseController() : this(null, null) { }

        protected BaseController(WebsiteDatabaseContext context) : this(context, null) { }

        protected BaseController(WebsiteDatabaseContext context, ApplicationUserManager manager)
        {
            Context = context;
            UserManager = manager;
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            HttpCookie cultureCookie = Request.Cookies["_culture"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = Request.UserLanguages != null && Request.UserLanguages.Length > 0 ?
                        Request.UserLanguages[0] :  // obtain it from HTTP header AcceptLanguages
                        null;
            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // This is safe
          
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(cultureName);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            return base.BeginExecuteCore(callback, state);
        }
    }
}