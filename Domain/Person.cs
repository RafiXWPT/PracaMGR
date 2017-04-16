using System;
using System.Runtime.Serialization;

namespace Domain
{
    public class Person
    {
        public Guid PersonId { get; set; }
        public string Pesel { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDate { get; set; }
        public string InsuranceId { get; set; }
        public virtual Address Address { get; set; }
    }

    [DataContract]
    public class PersonTransferObject
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
    }
}