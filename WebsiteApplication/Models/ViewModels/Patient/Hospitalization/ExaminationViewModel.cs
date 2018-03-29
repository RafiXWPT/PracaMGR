using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

        [Display(Name = "Osoba przeprowadzająca badanie")]
        public string Examinator { get; set; }

        [Display(Name = "Czy recepta wystawiona")]
        public bool SignedReceip { get; set; }

        public string UsedDevices { get; set; }

        [Display(Name = "Użyte przyrządy")]
        public List<string> SeparatedUsedDevices => UsedDevices.Split(',').ToList();

        [Display(Name = "Zwolnienie lekarskie")]
        public bool SickLeave { get; set; }

        [Display(Name = "Zwolnienie od")]
        public DateTime? SickLeaveFrom { get; set; }

        [Display(Name = "Zwolnienie do")]
        public DateTime? SickLeaveTo { get; set; }

        [Display(Name = "Wizyta prywatna")]
        public bool PrivateVisit { get; set; }

        public PersonViewModel Person { get; set; }
    }
}