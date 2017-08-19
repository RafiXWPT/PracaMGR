using System;

namespace WebsiteApplication.Models.ViewModels.Reports
{
    public class ReportRequestViewModel
    {
        public Guid ReaportRequestId { get; set; }
        public string PatientPesel { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}