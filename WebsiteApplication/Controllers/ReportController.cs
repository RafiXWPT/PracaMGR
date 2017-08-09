using System.Web.Mvc;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Raport;

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

        [AuthorizeRight(Right = "REAPORT_GENERATION")]
        public ActionResult GenerateReaport()
        {
            var tmp = "03300900942";
            var reaport = _reportService.GenerateRaport(tmp);
            if (reaport == null)
                return Json("Błąd połączenia", JsonRequestBehavior.AllowGet);
            return File(reaport, "pdf", "patientReaport.pdf");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult AcceptReaport()
        {
            return Json("");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult RejectReaport()
        {
            return Json("");
        }
    }
}