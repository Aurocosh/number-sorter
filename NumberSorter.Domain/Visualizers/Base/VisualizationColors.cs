using NumberSorter.Domain.Container;
using System.Windows.Media;

namespace NumberSorter.Domain.Visualizers
{
    public static class VisualizationColors
    {
        public static Color ReadColor => Colors.Yellow;
        public static Color WriteColor => Colors.Purple;
        public static Color NormalColor => Colors.White;

        public static Color CompareEqualColor => Colors.Blue;
        public static Color CompareBiggerColor => Colors.Red;
        public static Color CompareLesserColor => Colors.Green;

        public static Color GetColumnColor(SortState<int> sortState, int columnIndex)
        {
            if (columnIndex == sortState.FirstComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return CompareLesserColor;
                else if (sortState.ComparassionResult > 0)
                    return CompareBiggerColor;
                else
                    return CompareEqualColor;
            }
            else if (columnIndex == sortState.SecondComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return CompareBiggerColor;
                else if (sortState.ComparassionResult > 0)
                    return CompareLesserColor;
                else
                    return CompareEqualColor;
            }
            else if (columnIndex == sortState.ReadIndex)
            {
                return ReadColor;
            }
            else if (columnIndex == sortState.FirstWrittenIndex || columnIndex == sortState.SecondWrittenIndex)
            {
                return WriteColor;
            }
            else
            {
                return NormalColor;
            }
        }
    }
}
