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

        [Route("GetPatient/{pesel}")]
        public ActionResult GetPatient(string pesel)
        {
            var fetcher = new WcfDataFetcher(_repository);
            var patient = Mapper.Map<PatientBasicViewModel>(fetcher.GetPatient(pesel));
            return View(patient);
        }
    }
}