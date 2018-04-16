using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using Kendo.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.Classess;
using WebsiteApplication.CodeBehind.Helpers;
using WebsiteApplication.CodeBehind.WcfServices;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Models.ViewModels.GlobalData;
using WebsiteApplication.Models.ViewModels.Reports;

namespace WebsiteApplication.Controllers.Reports
{
    public class MineReportsListController : KendoController
    {
        private readonly WcfDataFetcher _patientFetcher;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IDateTimeCountableRepository<ReportRequest> _reportRequestRepository;
        private readonly IRepository<Institution> _institutionRepository;
        private readonly IDateTimeCountableRepository<SearchHistory> _searchHistoryRepository;

        public MineReportsListController(IDateTimeCountableRepository<ReportRequest> reportRequestRepository, IRepository<Institution> institutionRepository, IDateTimeCountableRepository<SearchHistory> searchHistoryRepository)
        {
            _reportRequestRepository = reportRequestRepository;
            _institutionRepository = institutionRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _patientFetcher = new WcfDataFetcher(institutionRepository, searchHistoryRepository, User.Name);
            _personInfoFetcher = new WcfPersonInfoFetcher();
        }

        public ActionResult MineReportsList()
        {
            return View();
        }

        public ActionResult MinePendingReports()
        {
            return PartialView();
        }

        public ActionResult MineAcceptedReports()
        {
            return PartialView();
        }

        public ActionResult MineRejectedReports()
        {
            return PartialView();
        }

        public ActionResult ReadMinePendingReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _reportRequestRepository.Entities.Where(r => r.Status == ReportRequestStatus.PENDING && r.CreatedBy == User.Name));
        }

        public ActionResult ReadMineAcceptedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _reportRequestRepository.Entities.Where(r => r.Status == ReportRequestStatus.ACCEPTED && r.CreatedBy == User.Name));
        }

        public ActionResult ReadMineRejectedReports(DataSourceRequest request)
        {
            return JsonDataSourceResult<ReportRequest, ReportRequestViewModel>(request,
                _reportRequestRepository.Entities.Where(r => r.Status == ReportRequestStatus.REJECTED && r.CreatedBy == User.Name));
        }

        public ActionResult FilterPersons([DataSourceRequest] DataSourceRequest request)
        {
            var filters = GetAllFilters(request);
            if (!filters.Any())
                return Json(new List<PersonViewModel>().ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

            return Json(_personInfoFetcher.FilterPersons(
                    (string) filters.FirstOrDefault(f => f.Member == "Pesel")?.Value,
                    (string) filters.FirstOrDefault(f => f.Member == "FirstName")?.Value,
                    (string) filters.FirstOrDefault(f => f.Member == "LastName")?.Value,
                    (string) filters.FirstOrDefault(f => f.Member == "InsuranceId")?.Value,
                    (DateTime?) filters.FirstOrDefault(f => f.Member == "BirthDate")?.Value).ToDataSourceResult(request),
                JsonRequestBehavior.AllowGet);
        }     

        public ActionResult NewRequestWindowContent()
        {
            return PartialView("EditorTemplates/_NewReportRequestTemplate");
        }

        [HttpPost]
        public ActionResult AddNewReportRequest(string patientPesel, string patientFirstName, string patientLastName)
        {
            if (TimeHelper.IsSearchCounterViolated(_reportRequestRepository, User.Name))
                return Json(OperationResult.FailureResult("Przekroczono ilość raportów"));

            var reportRequest = new ReportRequest
            {
                PatientPesel = patientPesel,
                PatientFirstName = patientFirstName,
                PatientLastName = patientLastName,
                CreatedAt = DateTime.Now,
                CreatedBy = User.Name,
                Status = ReportRequestStatus.PENDING
            };
            _reportRequestRepository.Create(reportRequest);

            return Json(OperationResult.SuccessResult());
        }

        [HttpPost]
        public ActionResult CanDownloadReport(Guid requestId)
        {
            var reportRequest = _reportRequestRepository.Read(requestId);
            if (reportRequest == null)
                return Json(OperationResult.FailureResult("Brak obiektu"));

            if (reportRequest.CreatedBy != User.Name)
                return Json(OperationResult.FailureResult("Raport należy do innego użytkownika"));

            if (reportRequest.Status != ReportRequestStatus.ACCEPTED)
                return Json(OperationResult.FailureResult("Raport nie został jeszcze zaakceptowany"));

            if (reportRequest.GeneratedReport == null)
                return Json(OperationResult.FailureResult("Nie można odczytac raportu"));

            return Json(OperationResult.SuccessResult());
        }

        public ActionResult DownloadReport(Guid requestId)
        {
            var reportRequest = _reportRequestRepository.Read(requestId);
            if (reportRequest == null)
                return Json(OperationResult.FailureResult("Brak obiektu"), JsonRequestBehavior.AllowGet);

            if (reportRequest.CreatedBy != User.Name)
                return Json(OperationResult.FailureResult("Raport należy do innego użytkownika"), JsonRequestBehavior.AllowGet);

            if (reportRequest.Status != ReportRequestStatus.ACCEPTED)
                return Json(OperationResult.FailureResult("Raport nie został jeszcze zaakceptowany"), JsonRequestBehavior.AllowGet);

            if (reportRequest.GeneratedReport == null)
                return Json(OperationResult.FailureResult("Nie można odczytac raportu"), JsonRequestBehavior.AllowGet);

            return File(reportRequest.GeneratedReport.Report, "pdf", $"{reportRequest.PatientPesel}_{DateTime.Now.ToShortDateString()}.pdf");
        }
    }
}