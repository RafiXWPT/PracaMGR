using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Domain.Inventory;

namespace Domain.Residence
{
    public class Treatment
    {
        public Guid TreatmentId { get; set; }
        public Guid HospitalizationId { get; set; }
        public DateTime TreatmentStartDate { get; set; }
        public DateTime TreatmentEndDate { get; set; }
        public virtual Hospitalization Hospitalization { get; set; }
        public virtual ICollection<UsedMedicine> UsedMedicines { get; set; }
    }

    [DataContract]
    public class TreatmentTransferObject
    {
        [DataMember]
        public Guid TreatmentId { get; set; }

        [DataMember]
        public DateTime TreatmentStartDate { get; set; }

        [DataMember]
        public DateTime TreatmentEndDate { get; set; }

        [DataMember]
        public List<UsedMedicineTransferObject> UsedMedicines { get; set; } = new List<UsedMedicineTransferObject>();
    }
}