using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class UsedMedicineViewModel
    {
        public Guid UsedMedicineId { get; set; }
        public double Dose { get; set; }
        public string MedicineMedicineName { get; set; }
    }
}