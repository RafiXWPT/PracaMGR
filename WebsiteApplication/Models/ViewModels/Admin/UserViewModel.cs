using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Admin
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime UserCreationDate { get; set; }
        public DateTime UserLastLogin { get; set; }

        [UIHint("RightsEditor")]
        public IEnumerable<RoleViewModel> UserRoles { get; set; }
    }
}