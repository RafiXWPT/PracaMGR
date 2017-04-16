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
        public Guid InstitutionId { get; set; }

        [DataMember]
        public string Pesel { get; set; }

        [DataMember]
        public string InstitutionName { get; set; }

        [DataMember]
        public List<HospitalizationBasicTransferObject> Hospitalizations { get; set; } =
            new List<HospitalizationBasicTransferObject>();
    }
}