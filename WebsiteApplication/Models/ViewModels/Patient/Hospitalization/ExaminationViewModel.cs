using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class ExaminationViewModel
    {
        public Guid InstitutionId { get; set; }
        public Guid ExaminationId { get; set; }

        [Display(Name = "Początek badania")]
        public DateTime ExaminationStartTime { get; set; }

        [Display(Name = "Zakończenie badania")]
        public DateTime ExaminationEndTime { get; set; }
    }

    public class ExaminationContainerViewModel
    {
        public Guid ExaminationId { get; set; }

        [Display(Name = "Początek badania")]
        public DateTime ExaminationStartTime { get; set; }

        [Display(Name = "Zakończenie badania")]
        public DateTime ExaminationEndTime { get; set; }

        [Display(Name = "Szczegóły badania")]
        public string ExaminationDetails { get; set; }
    }
}