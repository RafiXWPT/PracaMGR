using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using AutoMapper;

namespace WebsiteApplication.Models.ViewModels.Rights
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        [UIHint("RoleEditor")]
        public List<RoleViewModel> Roles { get; set; }
    }
}