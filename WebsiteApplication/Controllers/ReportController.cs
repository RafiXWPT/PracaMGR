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
        private readonly IRepository<ReportRequest> _reaportRequestRepository;

        public ReportController(IRepository<ReportRequest> reaportRequestRepository, IRaportService raportService)
        {
            _reaportRequestRepository = reaportRequestRepository;
            _raportService = raportService;
        }

        [HttpPost]
        [AuthorizeRight(Right = "REPORT_GENERATION")]
        public ActionResult AddReaport(string patientPesel, string patientFirstName, string patientLastName)
        {
            if (TimeHelper.IsCreatedCounterViolated(_reaportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono limit raportów"));

            var newReaportRequest = new ReportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = patientPesel,
                PatientFirstName = patientFirstName,
                PatientLastName = patientLastName,
                Status = ReportRequestStatus.PENDING
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeRight(Right = "FORCE_REPORT_GENERATION")]
        public ActionResult GenerateReaport(string patientPesel, string patientFirstName, string patientLastName)
        {
            if (TimeHelper.IsCreatedCounterViolated(_reaportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono limit raportów"));

            var newReaportRequest = new ReportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = patientPesel,
                PatientFirstName = patientFirstName,
                PatientLastName = patientLastName,
                Status = ReportRequestStatus.ACCEPTED,
                GeneratedReport = _raportService.GenerateRaport(patientPesel, User.Name)
            };

            _reaportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult());
        }
    }
}