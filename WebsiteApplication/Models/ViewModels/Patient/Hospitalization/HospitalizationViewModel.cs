using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class HospitalizationViewModel
    {
        public Guid HospitalizationId { get; set; }
        public DateTime HospitalizationStartTime { get; set; }
        public DateTime HospitalizationEndTime { get; set; }
        public List<ExaminationViewModel> Examinations { get; set; }
        public List<TreatmentViewModel> Treatments { get; set; }
    }
}
