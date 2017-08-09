using System.Collections.Generic;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class TabStripViewModel
    {
        public List<TabStripItemViewModel> TabStripItems { get; set; } = new List<TabStripItemViewModel>();
    }
}