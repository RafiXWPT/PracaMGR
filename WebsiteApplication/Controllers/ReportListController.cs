using System;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Raport;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Models.ViewModels.Reports;

namespace WebsiteApplication.Controllers
{
    [AuthorizeRight(Right = "REAPORT_ACCEPTANCE")]
    public class ReportListController : KendoController
    {
        private readonly IRepository<ReaportRequest> _repository;
        private readonly IRaportService _service;

        public ReportListController(IRepository<ReaportRequest> repository, IRaportService service)
        {
            _repository = repository;
            _service = service;
        }

        // GET: ReportList
        public ActionResult Index()
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
            return JsonDataSourceResult<ReaportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReaportRequestStatus.PENDING));
        }

        public ActionResult ReadAcceptedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReaportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReaportRequestStatus.ACCEPTED));
        }

        public ActionResult ReadRejectedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReaportRequest, ReportRequestViewModel>(request,
                _repository.Entities.Where(r => r.Status == ReaportRequestStatus.REJECTED));
        }

        [HttpPost]
        public ActionResult AcceptRequest(Guid requestId)
        {
            var request = _repository.Read(requestId);
            if (request == null)
                return Json(OperationResult.FailureResult("Obiekt nie istnieje"));

            request.Status = ReaportRequestStatus.ACCEPTED;
            request.GeneratedReaport = _service.GenerateRaport(request.PatientPesel, User.Name);
            _repository.Update(request);
            return Json(OperationResult.SuccessResult());
        }

        [HttpPost]
        public ActionResult RejectRequest(Guid requestId)
        {
            var request = _repository.Read(requestId);
            if (request == null)
                return Json(OperationResult.FailureResult("Obiekt nie istnieje"));

            request.Status = ReaportRequestStatus.REJECTED;
            _repository.Update(request);
            return Json(OperationResult.SuccessResult());
        }
    }
}