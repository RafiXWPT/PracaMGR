using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Residence
{
    public class Hospitalization
    {
        public Guid HospitalizationId { get; set; }
        public Guid PatientId { get; set; }
        public DateTime HospitalizationStartTime { get; set; }
        public DateTime HospitalizationEndTime { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual ICollection<Examination> Examinations { get; set; }
        public virtual ICollection<Treatment> Treatments { get; set; }
    }

    [DataContract]
    public class HospitalizationBasicTransferObject
    {
        [DataMember]
        public Guid HospitalizationId { get; set; }

        [DataMember]
        public DateTime HospitalizationStartTime { get; set; }

        [DataMember]
        public DateTime HospitalizationEndTime { get; set; }
    }

    [DataContract]
    public class HospitalizationTransferObject
    {
        [DataMember]
        public List<ExaminationBasicTransferObject> Examinations { get; set; } = new List<ExaminationBasicTransferObject>();

        [DataMember]
        public List<TreatmentBasicTransferObject> Treatments { get; set; } = new List<TreatmentBasicTransferObject>();
    }

    [DataContract]
    public class HospitalizationHistoryTransferObject
    {
        [DataMember]
        public Guid HospitalizationId { get; set; }

        [DataMember]
        public DateTime HospitalizationStartTime { get; set; }

        [DataMember]
        public DateTime HospitalizationEndTime { get; set; }

        [DataMember]
        public List<ExaminationTransferObject> Examinations { get; set; } = new List<ExaminationTransferObject>();

        [DataMember]
        public List<TreatmentTransferObject> Treatments { get; set; } = new List<TreatmentTransferObject>();
    }
}