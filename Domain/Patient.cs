using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [DataContract]
    public class Patient
    {
        [DataMember]
        public Guid PatientId { get; set; }
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
        public virtual Address Address { get; set; }
        [DataMember]
        public virtual ICollection<Hospitalization.Hospitalization> Hospitalizations { get; set; }
    }
}
