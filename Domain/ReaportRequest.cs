using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum ReaportRequestStatus
    {
        PENDING,
        ACCEPTED,
        REJECTED
    }

    public class ReaportRequest
    {
        public Guid ReaportRequestId { get; set; }
        public string PatientPesel { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public ReaportRequestStatus Status { get; set; }

        public virtual GeneratedReaport GeneratedReaport { get; set; }
    }
}
