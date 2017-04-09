using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class HospitalizationViewModel
    {
        public Guid HospitalizationId { get; set; }
        public Guid InstitutionId { get; set; }
        [Display(Name = "Początek hospitalizacji")]
        public DateTime HospitalizationStartTime { get; set; }
        [Display(Name = "Zakończenie hospitalizacji")]
        public DateTime HospitalizationEndTime { get; set; }
    }

    public class HospitalizationContainerViewModel
    {
        public PersonViewModel Person { get; set; }
        public List<ExaminationViewModel> Examinations { get; set; }
        public List<TreatmentViewModel> Treatments { get; set; }
    }
}
