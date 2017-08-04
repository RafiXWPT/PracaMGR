using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class GeneratedReaport
    {
        public Guid Id { get; set; }
        public string PatientPesel { get; set; }
        public byte[] Reaport { get; set; }
    }
}
