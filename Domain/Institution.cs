using System;
using System.ComponentModel.DataAnnotations;
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

    public class InstitutionTransferObject
    {
        public string InstitutionEndpointAddress { get; set; }
        public string InstitutionName { get; set; }
        public AddressTransferObject Address { get; set; }
    }
}