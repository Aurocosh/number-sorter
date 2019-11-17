using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using System;
using System.Linq;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Visualizers
{
    public class SquaresListVisualizer : IListVisualizer
    {
        private int ColumnSize { get; set; }
        private int SpacerSize { get; set; }

        private int MinColumnSize { get; }
        private int MinSpacerSize { get; }
        private float ColumnProportion { get; }

        public SquaresListVisualizer(int minColumnSize, int minSpacerSize, float columnProportion)
        {
            ColumnSize = minColumnSize;
            SpacerSize = minSpacerSize;
            MinColumnSize = minColumnSize;
            MinSpacerSize = minSpacerSize;
            ColumnProportion = columnProportion;
        }

        public WriteableBitmap Init(SortLog<int> sortLog, int width, int height)
        {
            int elementCount = Math.Max(1, sortLog.InputState.State.Count);
            int spacePerElement = width / elementCount;

            ColumnSize = (int)(spacePerElement * ColumnProportion);
            ColumnSize = Math.Max(ColumnSize, MinColumnSize);

            SpacerSize = spacePerElement - ColumnSize;
            SpacerSize = Math.Max(SpacerSize, MinSpacerSize);

            spacePerElement = ColumnSize + SpacerSize;
            int rawWidth = sortLog.InputState.State.Count * spacePerElement;
            if (rawWidth < width)
                rawWidth = width;
            return BitmapFactory.New(rawWidth, height);
        }

        public int Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet)
        {
            int width = writeableBitmap.PixelWidth;

            int yRange = writeableBitmap.PixelHeight - 20;
            int yOrigin = yRange + 10;

            using (writeableBitmap.GetBitmapContext())
            {
                writeableBitmap.Clear(colorSet.BackgroundColor);

                var list = sortState.State;
                if (list.Count == 0)
                    return 0;

                int spacePerElement = ColumnSize + SpacerSize;

                int shift = Math.Abs(Math.Min(list.Min(), 0));
                int maxPositive = list.Max();
                double scaleCoefficient = yRange / (double)(maxPositive + shift);

                int leftoverSpace = width - (list.Count * spacePerElement);
                int xCurrent = leftoverSpace / 2;
                for (int i = 0; i < list.Count; i++)
                {
                    var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i);
                    int scaledValue = (int)((list[i] + shift) * scaleCoefficient);
                    var yPos = yOrigin - scaledValue;
                    if (scaledValue > 0)
                        writeableBitmap.FillRectangle(xCurrent, yPos, xCurrent + ColumnSize, yPos + ColumnSize, currentColor);

                    xCurrent += spacePerElement;
                }
                return 0;
            }
        }
    }
}
