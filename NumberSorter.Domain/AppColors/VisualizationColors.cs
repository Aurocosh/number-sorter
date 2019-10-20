using NumberSorter.Domain.Container;
using System.Windows.Media;

namespace NumberSorter.Domain.AppColors
{
    public static class VisualizationColors
    {
        public static Color GetColumnColor(ColorSet colorSet, SortState<int> sortState, int columnIndex)
        {
            if (columnIndex == sortState.FirstComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return colorSet.CompareLesserColor;
                else if (sortState.ComparassionResult > 0)
                    return colorSet.CompareBiggerColor;
                else
                    return colorSet.CompareEqualColor;
            }
            else if (columnIndex == sortState.SecondComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return colorSet.CompareBiggerColor;
                else if (sortState.ComparassionResult > 0)
                    return colorSet.CompareLesserColor;
                else
                    return colorSet.CompareEqualColor;
            }
            else if (columnIndex == sortState.ReadIndex)
            {
                return colorSet.ReadColor;
            }
            else if (columnIndex == sortState.FirstWrittenIndex || columnIndex == sortState.SecondWrittenIndex)
            {
                return colorSet.WriteColor;
            }
            else
            {
                return colorSet.NormalColor;
            }
        }
    }
}
