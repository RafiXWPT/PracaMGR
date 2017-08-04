using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebsiteApplication.Models.ViewModels.Rights
{
    public class RightViewModel
    {
        public Guid Id { get; set; }
        public string RightName { get; set; }
        public string RightDescription { get; set; }
        [IgnoreMap]
        public List<RoleViewModel> Roles { get; set; }
    }
}