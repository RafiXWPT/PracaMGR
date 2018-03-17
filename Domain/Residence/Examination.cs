using System;
using System.Runtime.Serialization;

namespace Domain.Residence
{
    public class Examination
    {
        public Guid ExaminationId { get; set; }
        public Guid HospitalizationId { get; set; }
        public DateTime ExaminationStartTime { get; set; }
        public DateTime ExaminationEndTime { get; set; }
        public string ExaminationDetails { get; set; }
        public virtual Hospitalization Hospitalization { get; set; }
    }

    [DataContract]
    public class ExaminationTransferObject
    {
        [DataMember]
        public Guid ExaminationId { get; set; }

        [DataMember]
        public DateTime ExaminationStartTime { get; set; }

        [DataMember]
        public DateTime ExaminationEndTime { get; set; }

        [DataMember]
        public string ExaminationDetails { get; set; }
    }
}