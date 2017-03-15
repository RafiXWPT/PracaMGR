using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class ExaminationViewModel
    {
        public int ExaminationId { get; set; }
        public DateTime ExaminationStartTime { get; set; }
        public DateTime ExaminationEndTime { get; set; }
        public string ExaminationDetails { get; set; }
    }
}
