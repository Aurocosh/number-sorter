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

        public int Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet)
        {
            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height;
            int yOrigin = yRange - 20;

            writeableBitmap.Clear(colorSet.BackgroundColor);

            var list = sortState.State;

            if (list.Count == 0)
                return 0;

            int size = list.Count;
            int spacePerElement = width / size;
            if (spacePerElement == 0)
                spacePerElement = 1;

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
            int elementsFits = width / spacePerElement;
            int elementToDraw = Math.Min(list.Count, elementsFits);
            for (int i = 0; i < elementToDraw; i++)
            {
                var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i);
                int scaledValue = (int)((list[i] + shift) * scaleCoefficient);
                var yPos = yOrigin - scaledValue;
                if (scaledValue > 0)
                    writeableBitmap.FillEllipse(xCurrent, yPos, xCurrent + columnSize, yPos + columnSize, currentColor);

                xCurrent += columnSize + spacerSize;
            }
            return list.Count - elementToDraw;
        }
    }
}
