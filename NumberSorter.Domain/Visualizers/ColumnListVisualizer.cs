﻿using NumberSorter.Domain.AppColors;
using NumberSorter.Domain.Base.Visualizers;
using NumberSorter.Domain.Container;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace NumberSorter.Domain.Visualizers
{
    public class ColumnListVisualizer : IListVisualizer
    {
        private int ColumnSize { get; set; }
        private int SpacerSize { get; set; }

        private int MinColumnSize { get; }
        private int MinSpacerSize { get; }
        private float ColumnProportion { get; }

        public ColumnListVisualizer(int minColumnSize, int minSpacerSize, float columnProportion)
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

            int yRange = writeableBitmap.PixelHeight / 2;
            int yOrigin = yRange;

            writeableBitmap.Clear(colorSet.BackgroundColor);
            writeableBitmap.DrawLine(0, yOrigin, width, yOrigin, Colors.Gray);

            var list = sortState.State;

            if (list.Count == 0)
                return 0;

            int size = list.Count;
            int spacePerElement = ColumnSize + SpacerSize;

            int maxModule = list.Max(Math.Abs);
            double scaleCoefficient = yRange / (double)maxModule;

            int elementsFits = width / spacePerElement;
            int takenSpace = size * spacePerElement;
            int leftoverSpace = width - takenSpace;
            int xCurrent = leftoverSpace / 2;
            for (int i = 0; i < size; i++)
            {
                var currentColor = VisualizationColors.GetColumnColor(colorSet, sortState, i, out bool isNormal);

                int scaledValue = (int)(list[i] * scaleCoefficient);
                if (scaledValue > 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin - scaledValue, xCurrent + ColumnSize, yOrigin, currentColor);
                }
                else if (scaledValue < 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin + 1, xCurrent + ColumnSize, yOrigin - scaledValue, currentColor);
                }

                xCurrent += spacePerElement;
            }

            return 0;
        }
    }
}
