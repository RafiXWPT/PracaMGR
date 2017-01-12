using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public Guid AddressId { get; set; }
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
