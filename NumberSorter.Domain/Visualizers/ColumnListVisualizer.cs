using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Visualizers
{
    public class ColumnListVisualizer : IListVisualizer
    {
        private const int _minColumnSize = 1;
        private const int _desiredColumnSize = 30;

        private const int _minSpacerSize = 0;
        private const int _desiredSpacerSize = 5;

        private float _columnProportion = 0.8f;

        private Color _readColor = Colors.Yellow;
        private Color _writeColor = Colors.Purple;
        private Color _normalColor = Colors.White;

        private Color _compareEqualColor = Colors.Blue;
        private Color _compareBiggerColor = Colors.Red;
        private Color _compareLesserColor = Colors.Green;

        public void Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState)
        {
            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height / 2;
            int yOrigin = yRange;

            writeableBitmap.Clear(Colors.LightGray);
            writeableBitmap.DrawLine(0, yOrigin, width, yOrigin, Colors.Gray);

            var list = sortState.State;

            if (list.Count == 0)
                return;

            int size = list.Count;
            if (width < (_minColumnSize * size) + (_minSpacerSize * (size - 1)))
                return;

            int spacePerElement = width / size;

            int columnSize;
            int spacerSize;
            if (spacePerElement >= _desiredColumnSize + _desiredSpacerSize)
            {
                columnSize = _desiredColumnSize;
                spacerSize = _desiredSpacerSize;
            }
            else if (spacePerElement / _minColumnSize <= 1)
            {
                columnSize = 1;
                spacerSize = 0;
            }
            else
            {
                columnSize = (int)Math.Floor(spacePerElement * _columnProportion);
                spacerSize = spacePerElement - columnSize;
            }

            int maxModule = list.Max(Math.Abs);
            double scaleCoefficient = yRange / maxModule;

            int xCurrent = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var currentColor = GetColumnColor(sortState, i);

                int scaledValue = (int)(list[i] * scaleCoefficient);
                if (scaledValue > 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin - scaledValue, xCurrent + columnSize - 1, yOrigin, currentColor);
                }
                else if (scaledValue < 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin + 1, xCurrent + columnSize - 1, yOrigin - scaledValue, currentColor);
                }

                xCurrent += columnSize + spacerSize;
            }
        }

        private Color GetColumnColor(SortState<int> sortState, int columnIndex)
        {
            if (columnIndex == sortState.FirstComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return _compareLesserColor;
                else if (sortState.ComparassionResult > 0)
                    return _compareBiggerColor;
                else
                    return _compareEqualColor;
            }
            else if (columnIndex == sortState.SecondComparedIndex)
            {
                if (sortState.ComparassionResult < 0)
                    return _compareBiggerColor;
                else if (sortState.ComparassionResult > 0)
                    return _compareLesserColor;
                else
                    return _compareEqualColor;
            }
            else if (columnIndex == sortState.ReadIndex)
            {
                return _readColor;
            }
            else if (columnIndex == sortState.FirstWrittenIndex || columnIndex == sortState.SecondWrittenIndex)
            {
                return _writeColor;
            }
            else
            {
                return _normalColor;
            }
        }
    }
}
