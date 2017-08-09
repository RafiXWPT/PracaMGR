using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Interfaces;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using WebsiteApplication.CodeBehind.WcfServices;
using WebsiteApplication.Models.ViewModels.GlobalData;

namespace WebsiteApplication.Controllers
{
    public class GlobalDataController : BaseController
    {
        private readonly WcfDataFetcher _patientFetcher;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IInstitutionRepository _repository;

        public GlobalDataController(IInstitutionRepository repository)
        {
            _repository = repository;
            _patientFetcher = new WcfDataFetcher(repository);
            _personInfoFetcher = new WcfPersonInfoFetcher();
        }

        public ActionResult Index()
        {
            var tabStripViewModel = new TabStripViewModel();
            var institutions = _repository.Institutions.ToList();
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
            var ptos = _patientFetcher.GetAllPatientsFromInstitution(institutionId);
            foreach (var pto in ptos)
            {
                var personInfo = _personInfoFetcher.GetPersonInfo(pto.Pesel);
                displayList.Add(new PatientViewModel
                {
                    Pesel = pto.Pesel,
                    LastHospitalizationTime = pto.Hospitalizations.Any()
                        ? pto.Hospitalizations.Max(d => d.HospitalizationEndTime)
                        : (DateTime?) null,
                    Info = new PersonInfoViewModel
                    {
                        FirstName = personInfo.FirstName,
                        LastName = personInfo.SecondName,
                        BirthDate = personInfo.BirthDate
                    }
                });
            }

            return Json(displayList.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
    }
}