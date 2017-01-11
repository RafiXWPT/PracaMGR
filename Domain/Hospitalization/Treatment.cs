using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Inventory;

namespace Domain.Hospitalization
{
    [DataContract]
    public class Treatment
    {
        [DataMember]
        public Guid TreatmentId { get; set; }
        [DataMember]
        public Guid HospitalizationId { get; set; }
        [DataMember]
        public DateTime TreatmentDateTime { get; set; }
        [DataMember]
        public virtual Hospitalization Hospitalization { get; set; }
        [DataMember]
        public virtual ICollection<UsedMedicine> UsedMedicines { get; set; }
    }
}
