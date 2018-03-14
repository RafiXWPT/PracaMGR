using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PatientHistoryViewModel
    {
        [Display(Name = "Nazwa placówki")]
        public string InstitutionName { get; set; }

        [Display(Name = "Hospitalizacje")]
        public List<HospitalizationViewModel> Hospitalizations { get; set; } = new List<HospitalizationViewModel>();          
    }

    public class PatientHistoryContainerViewModel
    {
        public List<PatientHistoryViewModel> PatientHistory { get; set; } = new List<PatientHistoryViewModel>();
    }
}