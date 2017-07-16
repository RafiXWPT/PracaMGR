using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteApplication.Models.ViewModels.GlobalData
{
    public class TabStripViewModel
    {
        public List<TabStripItemViewModel> TabStripItems { get; set; } = new List<TabStripItemViewModel>();
    }
}