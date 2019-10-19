using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.QuickSort.PivotSelectors;
using NumberSorter.Domain.Base.Visualizers;
using System.Collections.Generic;

namespace NumberSorter.Domain.Visualizers
{
    public static class VisualizationFactory
    {
        public static IListVisualizer GetVisualizer(VisualizationType algorhythmType)
        {
            switch (algorhythmType)
            {
                case VisualizationType.Columns:
                    return new ColumnListVisualizer();
                case VisualizationType.PositiveColumns:
                    return new PositiveColumnListVisualizer();
                case VisualizationType.Points:
                    return new PointsListVisualizer();
                default:
                    return null;
            }
        }
    }
}