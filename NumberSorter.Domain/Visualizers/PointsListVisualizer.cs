using NumberSorter.Domain.AppColors;
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
    public class PointsListVisualizer : IListVisualizer
    {
        private const int _minColumnSize = 1;
        private const int _desiredColumnSize = 30;

        private const int _minSpacerSize = 0;
        private const int _desiredSpacerSize = 5;

        private float _columnProportion = 0.8f;

        public void Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet)
        {
            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height;
            int yOrigin = yRange - 20;

            writeableBitmap.Clear(colorSet.BackgroundColor);

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

            int shift = Math.Abs(Math.Min(list.Min(), 0));
            int maxPositive = list.Max();
            double scaleCoefficient = yRange / (maxPositive + shift);

            int xCurrent = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i);
                int scaledValue = (int)((list[i] + shift) * scaleCoefficient);
                var yPos = yOrigin - scaledValue;
                if (scaledValue > 0)
                    writeableBitmap.FillEllipse(xCurrent, yPos, xCurrent + columnSize - 1, yPos + columnSize - 1, currentColor);

                xCurrent += columnSize + spacerSize;
            }
        }
    }
}
