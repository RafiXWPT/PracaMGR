using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;
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