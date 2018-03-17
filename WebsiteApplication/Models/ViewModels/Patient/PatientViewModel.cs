using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PatientViewModel
    {
        public Guid PatientId { get; set; }

        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Display(Name = "Lista hospitalizacji")]
        public List<HospitalizationViewModel> Hospitalizations { get; set; } = new List<HospitalizationViewModel>();
    }
}