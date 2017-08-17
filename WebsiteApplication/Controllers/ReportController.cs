using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Raport;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRepository<ReaportRequest> _reaportRequestRepository;
        private readonly IRaportService _raportService;

        public ReportController(IRepository<ReaportRequest> reaportRequestRepository, IRaportService raportService)
        {
            _reaportRequestRepository = reaportRequestRepository;
            _raportService = raportService;
        }

        [HttpPost]
        [AuthorizeRight(Right = "REAPORT_GENERATION")]
        public ActionResult AddReaport(string pesel)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var requestsInLastMonth =
                _reaportRequestRepository.Entities.Count(r => r.CreatedAt > lastMonth);

            if (requestsInLastMonth > 10)
            {
                return Json(OperationResult.FailureResult("Przekroczono limit raportów na miesiąc"));
            }

            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.PENDING
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeRight(Right = "FORCE_REAPORT_GENERATION")]
        public ActionResult GenerateReaport(string pesel)
        {
            var lastMonth = DateTime.Now.AddMonths(-1);
            var requestsInLastMonth =
                _reaportRequestRepository.Entities.Count(r => r.CreatedAt > lastMonth);

            if (requestsInLastMonth > 10)
            {
                return Json(OperationResult.FailureResult("Przekroczono limit raportów na miesiąc"));
            }

            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.ACCEPTED,
                GeneratedReaport = _raportService.GenerateRaport(pesel)
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult());
        }
    }
}