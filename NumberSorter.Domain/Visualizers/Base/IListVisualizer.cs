using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Container;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Base.Visualizers
{
    public interface IListVisualizer
    {
        WriteableBitmap Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet, out int missingCount);
    }
}
