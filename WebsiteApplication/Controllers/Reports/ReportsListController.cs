using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Report;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Models.ViewModels.Reports;

namespace WebsiteApplication.Controllers.Reports
{
    [AuthorizeRight(Right = "REPORT_ACCEPTANCE")]
    public class ReportsListController : KendoController
    {
        private readonly IDateTimeCountableRepository<ReportRequest> _repository;
        private readonly IReportService _service;

        public ReportsListController(IDateTimeCountableRepository<ReportRequest> repository, IReportService service)
        {
            _repository = repository;
            _service = service;
        }

        // GET: ReportList
        public ActionResult ReportsList()
        {
            return View();
        }

        public ActionResult PendingReports()
        {
            return PartialView();
        }

        public ActionResult AcceptedReports()
        {
            return PartialView();
        }

        public ActionResult RejectedReports()
        {
            return PartialView();
        }

        public ActionResult ReadPendingReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReportRequestStatus.PENDING));
        }

        public ActionResult ReadAcceptedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReportRequestStatus.ACCEPTED));
        }

        public ActionResult ReadRejectedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReportRequestStatus.REJECTED));
        }

        [HttpPost]
        public ActionResult AcceptRequest(Guid requestId)
        {
            var request = _repository.Read(requestId);
            if (request == null)
                return Json(OperationResult.FailureResult("Obiekt nie istnieje"));

            request.Status = ReportRequestStatus.ACCEPTED;
            request.AcceptedAt = DateTime.Now;
            request.AcceptedBy = User.Name;
            request.GeneratedReport = _service.GenerateReport(request.PatientPesel, User.Name);
            _repository.Update(request);
            return Json(OperationResult.SuccessResult());
        }

        [HttpPost]
        public ActionResult RejectRequest(Guid requestId)
        {
            var request = _repository.Read(requestId);
            if (request == null)
                return Json(OperationResult.FailureResult("Obiekt nie istnieje"));

            request.Status = ReportRequestStatus.REJECTED;
            request.RejectedAt = DateTime.Now;
            request.RejectedBy = User.Name;
            _repository.Update(request);
            return Json(OperationResult.SuccessResult());
        }
    }
}