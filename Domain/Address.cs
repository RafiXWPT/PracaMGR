using System;
using System.Runtime.Serialization;

namespace Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string HomeNumber { get; set; }
    }

    [DataContract]
    public class AddressTransferObject
    {
        [DataMember]
        public string Country { get; set; }

        [DataMember]
        public string Province { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string Street { get; set; }

        [DataMember]
        public string ZipCode { get; set; }

        [DataMember]
        public string HomeNumber { get; set; }
    }
}