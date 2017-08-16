using System;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Raport;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRepository<ReaportRequest> _reaportRequestRepository;

        public ReportController(IRepository<ReaportRequest> reaportRequestRepository)
        {
            _reaportRequestRepository = reaportRequestRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeRight(Right = "REAPORT_GENERATION")]
        public ActionResult GenerateReaport(string pesel)
        {
            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.PENDING
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json("OK");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult AcceptReaport(Guid reaportId)
        {
            return Json("");
        }

        [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
        public ActionResult RejectReaport(Guid reaportId)
        {
            return Json("");
        }
    }
}