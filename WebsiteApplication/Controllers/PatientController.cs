using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind;
using WebsiteApplication.Models.ViewModels.Patient;
using System.Web.Routing;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Controllers
{
    [RoutePrefix("Patient")]
    public class PatientController : Controller
    {
        private readonly IInstitutionRepository _repository;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly WcfDataFetcher _patientInfoFetcher;
        public PatientController(IInstitutionRepository repository)
        {
            _repository = repository;
            _personInfoFetcher = new WcfPersonInfoFetcher();
            _patientInfoFetcher = new WcfDataFetcher(repository);
        }

        [HttpGet]
        public ActionResult Index(string pesel)
        {
            if(pesel == null)
            {
                return View();
            }
            else if (pesel.Length == 0)
            {
                ModelState.AddModelError("", "Nie podano numeru PESEL");
                return View();
            }
            else
            {
                if(pesel.All(x => Char.IsDigit(x)))
                {
                    return RedirectToAction("Patient", new RouteValueDictionary(new { controller = "Patient", action = "Patient", pesel = pesel }));
                }
                else
                {
                    ModelState.AddModelError("", "Wpisano niepoprawny numer PESEL");
                    return View();
                }
            }
        }

        [Route("Patient/{pesel}")]
        public ActionResult Patient(string pesel)
        {
            if (string.IsNullOrEmpty(pesel) && TempData.ContainsKey("LastSearchedPatient"))
                pesel = (TempData["LastSearchedPatient"] as PersonViewModel).Pesel;
            else
                TempData["LastSearchedPesel"] = pesel;

            return View();
        }

        [Route("Info/{pesel}")]
        public ActionResult GetPatientInfo(string pesel)
        {
            var person = Mapper.Map<PersonViewModel>(_personInfoFetcher.GetPersonInfo(pesel));
            if (person == null)
                return new HttpStatusCodeResult(69, "Osoba o podanym numerze PESEL nie istnieje.");

            TempData["LastSearchedPatient"] = person;

            return PartialView("_PersonInfo", person);
        }

        [Route("History/{pesel}")]
        public ActionResult GetPatientHistory(string pesel)
        {
            var patientRecordList = _patientInfoFetcher.GetPatientHistory(pesel).Select(Mapper.Map<PatientViewModel>).ToList();
            if (patientRecordList == null)
                return new HttpStatusCodeResult(69, "Dla tej osoby nie istnieją żadne wpisy na temat hospitalizacji.");

            if (patientRecordList.Count == 0)
                return new HttpStatusCodeResult(404);

            foreach(var patientRecord in patientRecordList)
            {
                patientRecord.Hospitalizations.ForEach(x => x.InstitutionId = patientRecord.InstitutionId);
            }

            return PartialView("_PersonHistory", patientRecordList);
        }

        [Route("HospitalizationDetails/{hospitalizationId}")]
        public ActionResult HospitalizationDetails(Guid hospitalizationId, Guid institutionId)
        {
            if(!TempData.ContainsKey("LastSearchedPatient"))
                return RedirectToAction("Patient", new RouteValueDictionary(new { controller = "Patient", action = "Patient", pesel = TempData["LastSearchedPesel"] }));

            var personViewModel = TempData["LastSearchedPatient"] as PersonViewModel;

            var institution = _repository.Institutions.First(x => x.InstitutionId == institutionId);
            var hospitalization = Mapper.Map<HospitalizationContainerViewModel>(_patientInfoFetcher.GetHospitalization(hospitalizationId, institution.InstitutionEndpointAddress));
            hospitalization.Person = personViewModel;
            
            return View(hospitalization);
        }
    }
}