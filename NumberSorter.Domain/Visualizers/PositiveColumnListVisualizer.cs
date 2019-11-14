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
    public class PositiveColumnListVisualizer : IListVisualizer
    {
        private int MinColumnSize { get; } = 1;
        private int DesiredColumnSize { get; } = 30;
        private int DesiredSpacerSize { get; } = 5;
        private float ColumnProportion { get; } = 0.8f;

        public PositiveColumnListVisualizer(int minColumnSize, int desiredColumnSize, int desiredSpacerSize, float columnProportion)
        {
            MinColumnSize = minColumnSize;
            DesiredColumnSize = desiredColumnSize;
            DesiredSpacerSize = desiredSpacerSize;
            ColumnProportion = columnProportion;
        }

        public int Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet)
        {
            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height;
            int yOrigin = yRange;

            writeableBitmap.Clear(colorSet.BackgroundColor);
            writeableBitmap.DrawLine(0, yOrigin, width, yOrigin, Colors.Gray);

            var list = sortState.State;

            if (list.Count == 0)
                return 0;

            int size = list.Count;
            int spacePerElement = width / size;
            if (spacePerElement == 0)
                spacePerElement = 1;

            int columnSize;
            int spacerSize;
            if (spacePerElement >= DesiredColumnSize + DesiredSpacerSize)
            {
                columnSize = DesiredColumnSize;
                spacerSize = DesiredSpacerSize;
            }
            else if (spacePerElement / MinColumnSize <= 1)
            {
                columnSize = 1;
                spacerSize = 0;
            }
            else
            {
                columnSize = (int)Math.Floor(spacePerElement * ColumnProportion);
                spacerSize = spacePerElement - columnSize;
            }

            int shift = Math.Abs(list.Min());
            int maxModule = list.Max(Math.Abs);
            double scaleCoefficient = (yRange - 10) / (maxModule + shift);

            int elementsFits = width / spacePerElement;
            int elementToDraw = Math.Min(list.Count, elementsFits);
            int takenSpace = elementToDraw * (columnSize + spacerSize);
            int leftoverSpace = width - takenSpace;
            int xCurrent = leftoverSpace / 2;
            for (int i = 0; i < elementToDraw; i++)
            {
                var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i);
                int scaledValue = (int)((list[i] + shift) * scaleCoefficient);
                if (scaledValue > 0)
                    writeableBitmap.FillRectangle(xCurrent, yOrigin - scaledValue, xCurrent + columnSize, yOrigin - 5, currentColor);

                xCurrent += columnSize + spacerSize;
            }
            return list.Count - elementToDraw;
        }
    }
}
