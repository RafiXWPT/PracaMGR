using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain;
using Domain.Interfaces;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.CodeBehind
{
    public class PdfRaportService : IRaportService
    {
        private readonly IInstitutionRepository _repository;
        private readonly WcfPersonInfoFetcher _personInfoFetcher;
        private readonly WcfDataFetcher _patientDataFetcher;

        public PdfRaportService(IInstitutionRepository repository)
        {
            _repository = repository;
            _personInfoFetcher = new WcfPersonInfoFetcher();
            _patientDataFetcher = new WcfDataFetcher(repository);
        }

        public byte[] GenerateRaport(string patientPesel)
        {
            var personInfo = _personInfoFetcher.GetPersonInfo(patientPesel);
            var patientHistory =_patientDataFetcher.GetPatientHistory(patientPesel);
           
            return null;
        }
    }
}