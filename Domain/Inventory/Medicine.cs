using System;
using System.Runtime.Serialization;

namespace Domain.Inventory
{
    [DataContract]
    public class Medicine
    {
        [DataMember]
        public Guid MedicineId { get; set; }
        [DataMember]
        public string MedicineName { get; set; }
    }
}
