using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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