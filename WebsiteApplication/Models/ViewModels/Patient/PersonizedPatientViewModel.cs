using System.Collections.Generic;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PersonizedPatientViewModel
    {
        public PersonViewModel PersonViewModel { get; set; }
        public List<PatientViewModel> PatientViewModels { get; set; }
    }
}
