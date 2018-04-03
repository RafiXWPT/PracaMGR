using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Org.BouncyCastle.Crypto.Paddings;

namespace WebsiteApplication.Models.ViewModels.Patient.Hospitalization
{
    public class HospitalizationDocumentViewModel
    {
        public Guid HospitalizationDocumentId { get; set; }
        public Guid InstitutionId { get; set; }
        public string Name { get; set; }
        public byte[] Content { get; set; }
    }
}