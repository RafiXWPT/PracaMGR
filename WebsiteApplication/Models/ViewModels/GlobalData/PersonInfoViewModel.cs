using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class PersonInfoViewModel
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        [Display(Name = "Data urodzenia")]
        public DateTime BirthDate { get; set; }
    }
}