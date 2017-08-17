using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GeneratedReaport
    {
        [ForeignKey("ReaportRequest")]
        public Guid GeneratedReaportId { get; set; }
        public string PatientPesel { get; set; }
        public DateTime GeneratedAt { get; set; }
        public byte[] Reaport { get; set; }

        public virtual ReaportRequest ReaportRequest { get; set; }
    }
}
