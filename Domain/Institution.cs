using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Institution
    {
        public Guid InstitutionId { get; set; }
        public string InstitutionEndpointAddress { get; set; }
        public virtual Address Address { get; set; }
    }
}
