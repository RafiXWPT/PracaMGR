using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;

namespace Domain
{
    public enum ReportRequestStatus
    {
        PENDING,
        ACCEPTED,
        REJECTED
    }

    public class ReportRequest
    {
        public Guid ReportRequestId { get; set; }
        public string PatientPesel { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientLastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public string AcceptedBy { get; set; }
        public DateTime? RejectedAt { get; set; }
        public string RejectedBy { get; set; }
        public ReportRequestStatus Status { get; set; }

        public virtual GeneratedReport GeneratedReport { get; set; }
    }
}
