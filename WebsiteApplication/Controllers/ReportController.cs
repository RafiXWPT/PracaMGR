using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteApplication.CodeBehind;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRaportService _reportService;

        public ReportController(IRaportService reportService)
        {
            _reportService = reportService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateReaport()
        {
            var tmp = "93070114133";
            var reaport = _reportService.GenerateRaport(tmp);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}