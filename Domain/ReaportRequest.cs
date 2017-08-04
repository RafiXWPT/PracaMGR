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
        public Guid Id { get; set; }
        public string PatientPesel { get; set; }
        public DateTime CreatedAt { get; set; }
        public ReaportRequestStatus Status { get; set; }
        public string Comment { get; set; }
    }
}
