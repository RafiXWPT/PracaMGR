using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using Domain.Residence;
using WebsiteApplication.CodeBehind.Attributes;
using WebsiteApplication.CodeBehind.Helpers;
using WebsiteApplication.CodeBehind.WcfServices;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Filters;
using WebsiteApplication.Models.ViewModels.Patient;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Controllers
{
    [RoutePrefix("Patient")]
    [AuthorizeRight(Right = "GET_PATIENT_INFO")]
    public class PatientController : KendoController
    {
        private readonly WcfDataFetcher _patientInfoFetcher;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IRepository<Institution> _institutionRepository;
        private readonly IRepository<SearchHistory> _searchHistoryRepository;

        public PatientController(IRepository<Institution> institutionRepository, IRepository<SearchHistory> searchHistoryRepository)
        {
            _institutionRepository = institutionRepository;
            _searchHistoryRepository = searchHistoryRepository;
            _personInfoFetcher = new WcfPersonInfoFetcher();
            _patientInfoFetcher = new WcfDataFetcher(institutionRepository, searchHistoryRepository, User.Name);
        }

        public ActionResult Index(string pesel)
        {
            if (pesel == null)
                return View();
            if (pesel.Length == 0)
            {
                ModelState.AddModelError("", "Nie podano numeru PESEL");
                return View();
            }
            if (pesel.All(x => char.IsDigit(x)))
                return RedirectToAction("Patient",
                    new RouteValueDictionary(new {controller = "Patient", action = "Patient", pesel}));
            ModelState.AddModelError("", "Wpisano niepoprawny numer PESEL");
            return View();
        }

        [Route("Info/{pesel}")]
        public ActionResult GetPatientInfo(string pesel)
        {
            var person = MapSingle<PersonTransferObject, PersonViewModel>(_personInfoFetcher.GetPersonInfo(pesel));
            if (person == null)
                return new HttpStatusCodeResult(69, "Osoba o podanym numerze PESEL nie istnieje.");

            TempData["CurrentPerson"] = person;

            return PartialView("_PersonInfo", person);
        }

        [Route("Patient/{pesel}")]
        public ActionResult Patient(string pesel)
        {
            if(TimeHelper.IsCreatedCounterViolated(_searchHistoryRepository, User.Name))
                return Json("Przekroczono ilość zapytań jaka jest dostępna", JsonRequestBehavior.AllowGet);

            return View();
        }

        [Route("History/{pesel}")]
        public ActionResult GetPatientHistory(string pesel)
        {
            if (TimeHelper.IsCreatedCounterViolated(_searchHistoryRepository, User.Name))
                return Json("Przekroczono ilość zapytań jaka jest dostępna", JsonRequestBehavior.AllowGet);

            var patientRecordList = _patientInfoFetcher.GetPatientInfo<PatientViewModel>(pesel);
            if (!patientRecordList.Any())
                return new HttpStatusCodeResult(69, "Dla tej osoby nie istnieją żadne wpisy na temat hospitalizacji.");

            if (patientRecordList.Count == 0)
                return new HttpStatusCodeResult(404);

            foreach (var patientRecord in patientRecordList)
                patientRecord.Hospitalizations.ForEach(x => x.InstitutionId = patientRecord.InstitutionId);

            return PartialView("_PersonHistory", patientRecordList);
        }

        [Route("HospitalizationDetails/{hospitalizationId}")]
        public ActionResult HospitalizationDetails(Guid hospitalizationId, Guid institutionId)
        {
            if (TimeHelper.IsCreatedCounterViolated(_searchHistoryRepository, User.Name))
                return Json("Przekroczono ilość zapytań jaka jest dostępna", JsonRequestBehavior.AllowGet);

            var personViewModel = TempData["CurrentPerson"] as PersonViewModel;
            if (personViewModel == null)
                return RedirectToAction("Index");

            TempData.Keep();

            var institution = _institutionRepository.Read(institutionId);
            var hospitalization = _patientInfoFetcher.GetHospitalization<HospitalizationContainerViewModel>(hospitalizationId, institution.InstitutionEndpointAddress);

            hospitalization.Person = personViewModel;
            hospitalization.Examinations.ForEach(x => x.InstitutionId = institutionId);
            hospitalization.Treatments.ForEach(x => x.InstitutionId = institutionId);

            return View(hospitalization);
        }

        [Route("ExaminationDetails/{treatmentId}")]
        public ActionResult ExaminationDetails(Guid examinationId, Guid institutionId)
        {
            if (TimeHelper.IsCreatedCounterViolated(_searchHistoryRepository, User.Name))
                return Json("Przekroczono ilość zapytań jaka jest dostępna", JsonRequestBehavior.AllowGet);

            var institution = _institutionRepository.Read(institutionId);
            var examination = _patientInfoFetcher.GetExamination<ExaminationContainerViewModel>(examinationId, institution.InstitutionEndpointAddress);
            return View(examination);
        }

        [Route("TreatmentDetails/{treatmentId}")]
        public ActionResult TreatmentDetails(Guid treatmentId, Guid institutionId)
        {
            if (TimeHelper.IsCreatedCounterViolated(_searchHistoryRepository, User.Name))
                return Json("Przekroczono ilość zapytań jaka jest dostępna", JsonRequestBehavior.AllowGet);

            var institution = _institutionRepository.Read(institutionId);
            var treatment = _patientInfoFetcher.GetTreatment<TreatmentContainerViewModel>(treatmentId, institution.InstitutionEndpointAddress);
            return View(treatment);
        }
    }
}