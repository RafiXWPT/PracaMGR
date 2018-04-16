using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class TreatmentViewModel
    {
        public Guid InstitutionId { get; set; }
        public Guid TreatmentId { get; set; }

        [Display(Name = "Początek operacji")]
        public DateTime TreatmentStartDate { get; set; }

        [Display(Name = "Zakończenie operacji")]
        public DateTime TreatmentEndDate { get; set; }

        [Display(Name = "Personel")]
        public string Personel { get; set; }
    }

    public class TreatmentContainerViewModel
    {
        public Guid TreatmentId { get; set; }

        [Display(Name = "Początek operacji")]
        public DateTime TreatmentStartDate { get; set; }

        [Display(Name = "Zakończenie operacji")]
        public DateTime TreatmentEndDate { get; set; }

        public string Personel { get; set; }
        public string UsedDevices { get; set; }

        [Display(Name = "Personel")]
        public List<string> SeparatedPersonel => Personel.Split(',').ToList();

        [Display(Name = "Użyte przyrządy")]
        public List<string> SeparatedUsedDevices => UsedDevices.Split(',').ToList();

        [Display(Name = "Użyte medykamenty")]
        public List<UsedMedicineViewModel> UsedMedicines { get; set; }

        public PersonViewModel Person { get; set; }
    }
}