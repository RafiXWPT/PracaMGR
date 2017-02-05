using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Residence;

namespace Domain
{
    public class Patient
    {
        public Guid PatientId { get; set; }
        public string Pesel { get; set; }
        public virtual ICollection<Hospitalization> Hospitalizations { get; set; }
    }

    [DataContract]
    public class PatientTransferObject
    {
        [DataMember]
        public string Pesel { get; set; }
        [DataMember]
        public string InstitutionName { get; set; }
        [DataMember]
        public List<HospitalizationTransferObject> Hospitalizations { get; set; } = new List<HospitalizationTransferObject>();
    }
}
