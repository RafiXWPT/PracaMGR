using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace WebsiteApplication.Models.ViewModels.Rights
{
    public class RightViewModel
    {
        public Guid Id { get; set; }
        [Display(Name = "Nazwa uprawnienia")]
        public string RightName { get; set; }
        [Display(Name = "Opis uprawnienia")]
        public string RightDescription { get; set; }

        [IgnoreMap]
        [UIHint("RoleEditor")]
        [Display(Name = "Role")]
        public List<RoleViewModel> Roles { get; set; }
    }
}