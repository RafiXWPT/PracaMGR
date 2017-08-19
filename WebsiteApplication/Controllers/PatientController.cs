using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind.Rights;
using WebsiteApplication.CodeBehind.WcfServices;
using WebsiteApplication.Controllers.AdditionalControllers;
using WebsiteApplication.Filters;
using WebsiteApplication.Models.ViewModels.Patient;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Controllers
{
    [RoutePrefix("Patient")]
    [RoleAuthorize(Roles = "ADMIN,DOCTOR")]
    public class PatientController : BaseController
    {
        private readonly WcfDataFetcher _patientInfoFetcher;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly IRepository<Institution> _repository;

        public PatientController(IRepository<Institution> repository)
        {
            _repository = repository;
            _personInfoFetcher = new WcfPersonInfoFetcher();
            _patientInfoFetcher = new WcfDataFetcher(repository);
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
            var person = Mapper.Map<PersonViewModel>(_personInfoFetcher.GetPersonInfo(pesel));
            if (person == null)
                return new HttpStatusCodeResult(69, "Osoba o podanym numerze PESEL nie istnieje.");

            TempData["CurrentPerson"] = person;

            return PartialView("_PersonInfo", person);
        }

        [Route("Patient/{pesel}")]
        public ActionResult Patient(string pesel)
        {
            return View();
        }

        [Route("History/{pesel}")]
        public ActionResult GetPatientHistory(string pesel)
        {
            var patientRecordList = _patientInfoFetcher.GetPatientInfo(pesel)
                .Select(Mapper.Map<PatientViewModel>)
                .ToList();
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
            var personViewModel = TempData["CurrentPerson"] as PersonViewModel;
            if (personViewModel == null)
                return RedirectToAction("Index");

            TempData.Keep();

            var institution = _repository.Read(institutionId);
            var hospitalization = Mapper.Map<HospitalizationContainerViewModel>(
                _patientInfoFetcher.GetHospitalization(hospitalizationId, institution.InstitutionEndpointAddress));

            hospitalization.Person = personViewModel;
            hospitalization.Examinations.ForEach(x => x.InstitutionId = institutionId);
            hospitalization.Treatments.ForEach(x => x.InstitutionId = institutionId);

            return View(hospitalization);
        }

        [Route("ExaminationDetails/{treatmentId}")]
        public ActionResult ExaminationDetails(Guid examinationId, Guid institutionId)
        {
            var institution = _repository.Read(institutionId);
            var examination =
                Mapper.Map<ExaminationContainerViewModel>(
                    _patientInfoFetcher.GetExamination(examinationId, institution.InstitutionEndpointAddress));
            return View(examination);
        }

        [Route("TreatmentDetails/{treatmentId}")]
        public ActionResult TreatmentDetails(Guid treatmentId, Guid institutionId)
        {
            var institution = _repository.Read(institutionId);
            var treatment =
                Mapper.Map<TreatmentContainerViewModel>(
                    _patientInfoFetcher.GetTreatment(treatmentId, institution.InstitutionEndpointAddress));
            return View(treatment);
        }
    }
}