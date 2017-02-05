using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Domain.Interfaces;
using WebsiteApplication.CodeBehind;
using WebsiteApplication.Models.ViewModels.Patient;

namespace WebsiteApplication.Controllers
{
    [RoutePrefix("Patients")]
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

        [Route("History/{pesel}")]
        public ActionResult GetHistory(string pesel)
        {
            var person = Mapper.Map<PersonViewModel>(_personInfoFetcher.GetPersonInfo(pesel));
            if (person != null)
            {
                var patientRecordList = new List<PatientViewModel>();
                foreach (var record in _patientInfoFetcher.GetPatientHistory(pesel))
                {
                    patientRecordList.Add(Mapper.Map<PatientViewModel>(record));
                }
                return View(new PersonizedPatientViewModel {PersonViewModel = person, PatientViewModels = patientRecordList });
            }

            return View();
        }
    }
}