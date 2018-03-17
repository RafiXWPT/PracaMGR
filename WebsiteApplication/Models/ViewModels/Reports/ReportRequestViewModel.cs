using System;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Reports
{
    public class ReportRequestViewModel
    {
        public Guid ReportRequestId { get; set; }
        [Display(Name = "PESEL pacjenta")]
        public string PatientPesel { get; set; }
        [Display(Name = "Imię pacjenta")]
        public string PatientFirstName { get; set; }
        [Display(Name = "Nazwisko pacjenta")]
        public string PatientLastName { get; set; }
        [Display(Name = "Utworzony do akceptacji")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [Display(Name = "Utworzony przez")]
        public string CreatedBy { get; set; }
        [Display(Name = "Zaakceptowany")]
        [DataType(DataType.DateTime)]
        public DateTime? AcceptedAt { get; set; }
        [Display(Name = "Zaakceptowany przez")]
        public string AcceptedBy { get; set; }
        [Display(Name = "Odrzucony")]
        [DataType(DataType.DateTime)]
        public DateTime? RejectedAt { get; set; }
        [Display(Name = "Odrzucony przez")]
        public string RejectedBy { get; set; }
    }
}