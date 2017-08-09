using System;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class PersonInfoViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime BirthDate { get; set; }
    }
}