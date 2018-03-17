using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Domain.Interfaces;

namespace Domain
{
    public class Institution
    {
        public Guid InstitutionId { get; set; }
        public string InstitutionEndpointAddress { get; set; }
        public string InstitutionName { get; set; }
        public virtual Address Address { get; set; }
    }

    [DataContract]
    public class InstitutionTransferObject
    {
        [DataMember]
        public Guid InstitutionId { get; set; }

        [DataMember]
        public string InstitutionEndpointAddress { get; set; }

        [DataMember]
        public string InstitutionName { get; set; }

        [DataMember]
        public AddressTransferObject Address { get; set; }
    }

    [DataContract]
    public class InstitutionPatientsTransferObject
    {
        [DataMember]
        public Guid InstitutionId { get; set; }

        [DataMember]
        public List<PatientTransferObject> Patients { get; set; } = new List<PatientTransferObject>();
    }
}