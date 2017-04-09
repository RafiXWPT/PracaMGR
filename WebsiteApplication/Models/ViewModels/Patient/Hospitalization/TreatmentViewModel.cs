using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class TreatmentViewModel
    {
        public Guid TreatmentId { get; set; }
        [Display(Name = "Data przeprowadzenia leczenia")]
        public DateTime TreatmentDateTime { get; set; }
    }

    public class TreatmentContainerViewModel
    {
        public Guid TreatmentId { get; set; }
        [Display(Name = "Data przeprowadzenia leczenia")]
        public DateTime TreatmentDateTime { get; set; }
    }
}
