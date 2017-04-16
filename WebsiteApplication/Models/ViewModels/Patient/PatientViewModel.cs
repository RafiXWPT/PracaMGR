using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PatientViewModel
    {
        public Guid InstitutionId { get; set; }

        [Display(Name = "Nazwa instytucji")]
        public string InstitutionName { get; set; }

        [Display(Name = "Lista hospitalizacji")]
        public List<HospitalizationViewModel> Hospitalizations { get; set; }
    }
}