using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Helpers;
using WebsiteApplication.CodeBehind.Report;
using WebsiteApplication.Controllers.AdditionalControllers;

namespace WebsiteApplication.Controllers
{
    public class ReportController : BaseController
    {
        private readonly IReportService _reportService;
        private readonly IDateTimeCountableRepository<ReportRequest> _reportRequestRepository;

        public ReportController(IDateTimeCountableRepository<ReportRequest> reportRequestRepository, IReportService reportService)
        {
            _reportRequestRepository = reportRequestRepository;
            _reportService = reportService;
        }

        [HttpPost]
        [AuthorizeRight(Right = "REPORT_GENERATION")]
        public ActionResult AddReport(string patientPesel, string patientFirstName, string patientLastName)
        {
            if (TimeHelper.IsSearchCounterViolated(_reportRequestRepository, User.Name))
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

            _reportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AuthorizeRight(Right = "FORCE_REPORT_GENERATION")]
        public ActionResult GenerateReport(string patientPesel, string patientFirstName, string patientLastName)
        {
            if (TimeHelper.IsSearchCounterViolated(_reportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono limit raportów"));

            var newReaportRequest = new ReportRequest
            {
                CreatedAt = DateTime.Now,
                CreatedBy = User.Identity.Name,
                PatientPesel = patientPesel,
                PatientFirstName = patientFirstName,
                PatientLastName = patientLastName,
                Status = ReportRequestStatus.ACCEPTED,
                GeneratedReport = _reportService.GenerateReport(patientPesel, User.Name)
            };

            _reportRequestRepository.Create(newReaportRequest);
            return Json(OperationResult.SuccessResult());
        }
    }
}