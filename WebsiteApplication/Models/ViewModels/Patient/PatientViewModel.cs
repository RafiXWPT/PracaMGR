using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteApplication.Models.ViewModels.Patient.Hospitalization;

namespace WebsiteApplication.Models.ViewModels.Patient
{
    public class PatientViewModel
    {
        public Guid InstitutionId { get; set; }
        public string InstitutionName { get; set; }
        public List<HospitalizationViewModel> Hospitalizations { get; set; }
    }
}
