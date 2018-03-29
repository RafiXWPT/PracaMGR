using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Residence
{
    public class HospitalizationDocument
    {
        public Guid HospitalizationDocumentId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
        public Guid HospitalizationId { get; set; }
        public virtual Hospitalization Hospitalization { get; set; }
    }

    [DataContract]
    public class HospitalizationDocumentTransferObject
    {
        [DataMember]
        public Guid HospitalizationDocumentId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public byte[] Content { get; set; }
    }
}
