using NumberSorter.Domain.Base.Visualizers;

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