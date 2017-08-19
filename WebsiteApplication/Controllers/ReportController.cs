using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Helpers;
using WebsiteApplication.CodeBehind.Raport;
using WebsiteApplication.Controllers.AdditionalControllers;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IRaportService _raportService;
        private readonly IRepository<ReaportRequest> _reaportRequestRepository;

        public ReportController(IRepository<ReaportRequest> reaportRequestRepository, IRaportService raportService)
        {
            _reaportRequestRepository = reaportRequestRepository;
            _raportService = raportService;
        }

        [HttpPost]
        [AuthorizeRight(Right = "REAPORT_GENERATION")]
        public ActionResult AddReaport(string pesel)
        {
            if (TimeHelper.IsCreatedCounterViolated(_reaportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono limit raportów"));

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
            if (TimeHelper.IsCreatedCounterViolated(_reaportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono limit raportów"));

            var newReaportRequest = new ReaportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = pesel,
                Status = ReaportRequestStatus.ACCEPTED,
                GeneratedReaport = _raportService.GenerateRaport(pesel, User.Name)
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult());
        }
    }
}