using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Hospitalization
{
    [DataContract]
    public class Hospitalization
    {
        [DataMember]
        public Guid HospitalizationId { get; set; }
        [DataMember]
        public Guid PatientId { get; set; }
        [DataMember]
        public DateTime HospitalizationStartTime { get; set; }
        [DataMember]
        public DateTime HospitalizationEndTime { get; set; }
        [DataMember]
        public virtual Patient Patient { get; set; }
        [DataMember]
        public virtual ICollection<Examination> Examinations { get; set; }
        [DataMember]
        public virtual ICollection<Treatment> Treatments { get; set; }           
    }
}
