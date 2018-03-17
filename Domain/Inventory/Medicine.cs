using System;
using System.Runtime.Serialization;

namespace Domain.Inventory
{
    public class Medicine
    {
        public Guid MedicineId { get; set; }
        public string MedicineName { get; set; }
    }

    [DataContract]
    public class MedicineTransferObject
    {
        [DataMember]
        public Guid MedicineId { get; set; }

        [DataMember]
        public string MedicineName { get; set; }
    }
}