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
        public PatientController(IInstitutionRepository repository)
        {
            _repository = repository;
        }

        [Route("History/{pesel}")]
        public ActionResult GetHistory(string pesel)
        {
            throw new NotImplementedException();
            /*var fetcher = new WcfDataFetcher(_repository);
            var patient = Mapper.Map<PersonViewModel>(fetcher.GetPatientHistory(pesel));
            return View(patient);*/
        }

        [Route("Person/{pesel}")]
        public ActionResult GetPersonInfo(string pesel)
        {
            var wcf = new WcfPersonInfoFetcher();
            var person = wcf.GetPersonInfo(pesel);
            return View();
        }
    }
}