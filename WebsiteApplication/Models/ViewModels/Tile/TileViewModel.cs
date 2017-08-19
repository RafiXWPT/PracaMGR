using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteApplication.Models.ViewModels.Tile
{
    public class TileViewModel
    {
        public string TileUrl { get; set; } = "#";
        public string TileIcon { get; set; }
        public string TileContentText { get; set; }
        public string TileValue { get; set; }
        public string TileColor { get; set; }
        public bool Break { get; set; }
        public string BreakHeader { get; set; }
    }
}