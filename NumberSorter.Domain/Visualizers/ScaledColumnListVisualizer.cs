using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Visualizers
{
    public class ScaledColumnListVisualizer : IListVisualizer
    {
        private int ColumnSize { get; }
        private int SpacerSize { get; }
        private WriteableBitmap RawBitmap { get; set; }

        public ScaledColumnListVisualizer(int columnSize, int spacerSize)
        {
            ColumnSize = columnSize;
            SpacerSize = spacerSize;
            RawBitmap = BitmapFactory.New(10, 10);
        }

        public WriteableBitmap Redraw(WriteableBitmap writeableBitmap, SortState<int> sortState, ColorSet colorSet, out int missingCount)
        {
            int spacePerElement = ColumnSize + SpacerSize;
            int rawWidth = sortState.State.Count * spacePerElement;
            if (rawWidth == 0)
                rawWidth = 10;

            if (rawWidth != RawBitmap.PixelWidth)
                RawBitmap = BitmapFactory.New(rawWidth, writeableBitmap.PixelHeight);

            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height / 2;
            int yOrigin = yRange;

            RawBitmap.Clear(colorSet.BackgroundColor);
            RawBitmap.DrawLine(0, yOrigin, rawWidth, yOrigin, Colors.Gray);

            var list = sortState.State;
            missingCount = 0;
            if (list.Count == 0)
                return writeableBitmap;

            int size = list.Count;
            int maxModule = list.Max(Math.Abs);
            double scaleCoefficient = yRange / maxModule;

            int xCurrent = 0;
            for (int i = 0; i < list.Count; i++)
            {
                var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i);

                int scaledValue = (int)(list[i] * scaleCoefficient);
                if (scaledValue > 0)
                {
                    RawBitmap.FillRectangle(xCurrent, yOrigin - scaledValue, xCurrent + ColumnSize, yOrigin, currentColor);
                }
                else if (scaledValue < 0)
                {
                    RawBitmap.FillRectangle(xCurrent, yOrigin + 1, xCurrent + ColumnSize, yOrigin - scaledValue, currentColor);
                }

                xCurrent += ColumnSize + SpacerSize;
            }

            return RawBitmap.Resize(writeableBitmap.PixelWidth, writeableBitmap.PixelHeight, WriteableBitmapExtensions.Interpolation.NearestNeighbor);
        }
    }
}
