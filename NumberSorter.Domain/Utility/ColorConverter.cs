using DrawingColor = System.Drawing.Color;
using MediaColor = System.Windows.Media.Color;

namespace NumberSorter.Domain.Utility
{
    public static class ColorConverter
    {
        public static MediaColor ConvertToMedia(this DrawingColor color) => MediaColor.FromArgb(color.A, color.R, color.G, color.B);
        public static DrawingColor ConvertToDrawing(this MediaColor color) => DrawingColor.FromArgb(color.A, color.R, color.G, color.B);
    }
}
