using System;
using System.Runtime.Serialization;
using Domain.Hospitalization;

namespace Domain.Inventory
{
    [DataContract]
    public class UsedMedicine
    {
        [DataMember]
        public Guid UsedMedicineId { get; set; }
        [DataMember]
        public Guid TreatmentId { get; set; }
        [DataMember]
        public Guid MedicineId { get; set; }
        [DataMember]
        public double Dose { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Medicine Medicine { get; set; }
    }
}
