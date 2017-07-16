using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Domain;
using WebsiteApplication.Resources;

namespace WebsiteApplication.Models.ViewModels.Institution
{
    public class InstitutionViewModel
    {
        public Guid InstitutionId { get; set; }
        [Display(Name = "InstitutionName", ResourceType = typeof(GlobalTranslations))]
        public string InstitutionName { get; set; }
        [Display(Name = "InstitutionEndpointAddress", ResourceType = typeof(GlobalTranslations))]
        public string InstitutionEndpointAddress { get; set; }

        public Address Address { get; set; }
    }
}