using System;
using System.ComponentModel.DataAnnotations;
using WebsiteApplication.Resources;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class PersonViewModel
    {
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }

        [Display(Name = "Number ubezpieczenia")]
        public string InsuranceId { get; set; }

        [Display(Name = "PersonBirthDate", ResourceType = typeof(GlobalTranslations))]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "LastHospitalizationTime", ResourceType = typeof(GlobalTranslations))]
        public DateTime? LastHospitalizationTime { get; set; }
    }
}