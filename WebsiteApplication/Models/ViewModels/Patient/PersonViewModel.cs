using System;
using System.ComponentModel.DataAnnotations;
using WebsiteApplication.Resources;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PersonViewModel
    {
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        [Display(Name = "PersonFirstName", ResourceType = typeof(GlobalTranslations))]
        public string FirstName { get; set; }

        [Display(Name = "PersonSecondName", ResourceType = typeof(GlobalTranslations))]
        public string LastName { get; set; }

        [Display(Name = "PersonBirthDate", ResourceType = typeof(GlobalTranslations))]
        public DateTime BirthDate { get; set; }

        [Display(Name = "PersonInsuranceNumber", ResourceType = typeof(GlobalTranslations))]
        public string InsuranceId { get; set; }

        [Display(Name = nameof(AddressCountry), ResourceType = typeof(GlobalTranslations))]
        public string AddressCountry { get; set; }

        [Display(Name = nameof(AddressProvince), ResourceType = typeof(GlobalTranslations))]
        public string AddressProvince { get; set; }

        [Display(Name = nameof(AddressCity), ResourceType = typeof(GlobalTranslations))]
        public string AddressCity { get; set; }

        [Display(Name = nameof(AddressZipCode), ResourceType = typeof(GlobalTranslations))]
        public string AddressZipCode { get; set; }

        [Display(Name = nameof(AddressStreet), ResourceType = typeof(GlobalTranslations))]
        public string AddressStreet { get; set; }

        [Display(Name = "PersonHomeNumber")]
        public string AddressHomeNumber { get; set; }
    }
}