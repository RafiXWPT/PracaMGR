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
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string InsuranceId { get; set; }
        public virtual Address Address { get; set; }
        public virtual ICollection<Hospitalization> Hospitalizations { get; set; }
    }

    [DataContract]
    public class PatientTransferObject
    {
        [DataMember]
        public string Pesel { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string SecondName { get; set; }
        [DataMember]
        public DateTime BirthDate { get; set; }
        [DataMember]
        public string InsuranceId { get; set; }
        [DataMember]
        public AddressTransferObject Address { get; set; }
        [DataMember]
        public List<HospitalizationTransferObject> Hospitalizations { get; set; }
    }
}
