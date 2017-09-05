using System.ComponentModel.DataAnnotations;

namespace WebsiteApplication.Models.ViewModels.Rights
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Display(Name = "Nazwa roli")]
        public string Name { get; set; }
    }
}