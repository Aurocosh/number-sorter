using NumberSorter.Domain.Base.Visualizers;
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

        public void Redraw(WriteableBitmap writeableBitmap, IReadOnlyList<int> list)
        {
            int width = (int)Math.Floor(writeableBitmap.Width);
            int height = (int)Math.Floor(writeableBitmap.Height);

            int yRange = height / 2;
            int yOrigin = yRange;

            writeableBitmap.Clear(Colors.LightGray);
            writeableBitmap.DrawLine(0, yOrigin, width, yOrigin, Colors.Gray);

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

            Color columnColor = Colors.White;

            int xCurrent = 0;
            foreach (int value in list)
            {
                int scaledValue = (int)(value * scaleCoefficient);
                if (scaledValue > 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin - scaledValue, xCurrent + columnSize - 1, yOrigin, columnColor);
                }
                else if (scaledValue < 0)
                {
                    writeableBitmap.FillRectangle(xCurrent, yOrigin + 1, xCurrent + columnSize - 1, yOrigin - scaledValue, columnColor);
                }

                xCurrent += columnSize + spacerSize;
            }
        }
    }
}
