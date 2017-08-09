using System;
using System.ComponentModel.DataAnnotations;
using WebsiteApplication.Resources;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class PatientViewModel
    {
        [Display(Name = "PESEL")]
        public string Pesel { get; set; }

        public PersonInfoViewModel Info { get; set; }

        [Display(Name = "LastHospitalizationTime", ResourceType = typeof(GlobalTranslations))]
        public DateTime? LastHospitalizationTime { get; set; }

        [Display(Name = "PersonBirthDate", ResourceType = typeof(GlobalTranslations))]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate => Info.BirthDate;

        [Display(Name = "Patient", ResourceType = typeof(GlobalTranslations))]
        public string FullName => Info.FullName;
    }
}