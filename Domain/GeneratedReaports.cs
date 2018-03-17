using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GeneratedReport
    {
        [ForeignKey("ReportRequest")]
        public Guid GeneratedReportId { get; set; }
        public string PatientPesel { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] Report { get; set; }

        public virtual ReportRequest ReportRequest { get; set; }
    }
}
