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
            _patientInfoFetcher = new WcfDataFetcher(_repository);
        }

        [Route("{pesel}")]
        public ActionResult Patient(string pesel)
        {
            return View();
        }

        [Route("Info/{pesel}")]
        public ActionResult GetPatientInfo(string pesel)
        {
            var person = Mapper.Map<PersonViewModel>(_personInfoFetcher.GetPersonInfo(pesel));
            return PartialView("_PersonInfo", person);
        }

        [Route("History/{pesel}")]
        public ActionResult GetPatientHistory(string pesel)
        {
            var patientRecordList = _patientInfoFetcher.GetPatientHistory(pesel).Select(Mapper.Map<PatientViewModel>).ToList();
            return PartialView("_PersonHistory", patientRecordList);
        }

        [Route("HospitalizationDetails/{hospitalizationId}")]
        public ActionResult HospitalizationDetails(Guid hospitalizationId)
        {
            return View();
        }
    }
}