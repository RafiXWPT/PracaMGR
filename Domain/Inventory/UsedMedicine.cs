using System;
using System.Runtime.Serialization;
using Domain.Residence;

namespace Domain.Inventory
{
    public class UsedMedicine
    {
        public Guid UsedMedicineId { get; set; }
        public Guid TreatmentId { get; set; }
        public Guid MedicineId { get; set; }
        public double Dose { get; set; }
        public virtual Treatment Treatment { get; set; }
        public virtual Medicine Medicine { get; set; }
    }

    [DataContract]
    public class UsedMedicineTransferObject
    {
        [DataMember]
        public double Dose { get; set; }
        [DataMember]

        public MedicineTransferObject Medicine { get; set; }
    }
}