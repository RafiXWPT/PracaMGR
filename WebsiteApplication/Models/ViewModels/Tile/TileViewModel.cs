namespace WebsiteApplication.Models.ViewModels.Tile
{
    public class TileViewModel : TileBase
    {
        public string TileUrl { get; set; }
        public string TileIcon { get; set; }
        public string TileContentText { get; set; }
        public string TileValue { get; set; }
        public string TileCircleColor { get; set; }
        public string TileContentColor { get; set; }
        public string Tooltip { get; set; }
    }
}