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
        public IEnumerable<RightViewModel> UserRoles { get; set; }
    }

    public class RightViewModel
    {
        public string RightId { get; set; }
        public string RightDescription { get; set; }
    }
}