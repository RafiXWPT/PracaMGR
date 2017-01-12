using System;
using System.Runtime.Serialization;

namespace Domain.Hospitalization
{
    [DataContract]
    public class Examination
    {
        [DataMember]
        public Guid ExaminationId { get; set; }
        [DataMember]
        public Guid HospitalizationId { get; set; }
        [DataMember]
        public DateTime ExaminationStartTime { get; set; }
        [DataMember]
        public DateTime ExaminationEndTime { get; set; }
        [DataMember]
        public string ExaminationDetails { get; set; }
        public virtual Hospitalization Hospitalization { get; set; }
    }
}
