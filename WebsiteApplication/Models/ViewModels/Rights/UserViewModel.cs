using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Rights
{
    public class UserViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nazwa użytkownika")]
        public string Name { get; set; }

        [UIHint("RoleEditor")]
        [Display(Name = "Role")]
        public List<RoleViewModel> Roles { get; set; }
    }
}