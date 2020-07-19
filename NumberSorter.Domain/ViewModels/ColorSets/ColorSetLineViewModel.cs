using NumberSorter.Domain.AppColors;
using ReactiveUI;
using System.Windows.Media;

namespace NumberSorter.Domain.ViewModels
{
    public class ColorSetLineViewModel : ReactiveObject
    {
        public string Name => ColorSet.Name;

        public Color ReadColor => ColorSet.ReadColor;
        public Color WriteColor => ColorSet.WriteColor;
        public Color NormalColor => ColorSet.NormalColor;

        public Color BackgroundColor => ColorSet.BackgroundColor;

        public Color CompareEqualColor => ColorSet.CompareEqualColor;
        public Color CompareBiggerColor => ColorSet.CompareBiggerColor;
        public Color CompareLesserColor => ColorSet.CompareLesserColor;

        public ColorSet ColorSet { get; }

        public ColorSetLineViewModel(ColorSet colorSet)
        {
            ColorSet = colorSet;
        }
    }
}
