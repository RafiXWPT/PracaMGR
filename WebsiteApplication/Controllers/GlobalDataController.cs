using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain;
using Domain.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.WcfServices;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Models.ViewModels.GlobalData;

namespace WebsiteApplication.Controllers
{
    public class GlobalDataController : KendoController
    {
        private readonly WcfDataFetcher _patientFetcher;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IRepository<Institution> _institutionRepository;

        public GlobalDataController(IRepository<Institution> institutionRepository, IDateTimeCountableRepository<SearchHistory> searchHistoryRepository)
        {
            _institutionRepository = institutionRepository;
            _patientFetcher = new WcfDataFetcher(institutionRepository, searchHistoryRepository, User.Name);
            _personInfoFetcher = new WcfPersonInfoFetcher();
        }

        public ActionResult Index()
        {
            var tabStripViewModel = new TabStripViewModel();
            var institutions = _institutionRepository.Entities.ToList();
            foreach (var institution in institutions)
                tabStripViewModel.TabStripItems.Add(new TabStripItemViewModel
                {
                    InstitutionId = institution.InstitutionId,
                    InstitutionName = institution.InstitutionName
                });
            return View(tabStripViewModel);
        }

        public ActionResult ReadTabItem(TabStripItemViewModel viewModel)
        {
            return PartialView("TabItem", viewModel);
        }

        public ActionResult ReadInstitutionData(DataSourceRequest request, Guid institutionId)
        {
            var displayList = new List<PatientViewModel>();
            var ptos = _patientFetcher.GetAllPatientsFromInstitution<PatientTransferObject>(institutionId);
            foreach (var pto in ptos)
            {
                var personInfo = _personInfoFetcher.GetPersonInfo(pto.Pesel);
                displayList.Add(new PatientViewModel
                {
                    Pesel = pto.Pesel,
                    LastHospitalizationTime = pto.Hospitalizations.Any()
                        ? pto.Hospitalizations.Max(d => d.HospitalizationEndTime)
                        : (DateTime?) null,
                    FirstName = personInfo.FirstName,
                    LastName = personInfo.LastName,
                    BirthDate = personInfo.BirthDate
                });
            }

            return JsonDataSourceResult(request, displayList);
        }
    }
}