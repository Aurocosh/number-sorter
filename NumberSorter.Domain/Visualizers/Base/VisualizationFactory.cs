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
                    return new ColumnListVisualizer(1, 30, 5, 0.8f);
                case VisualizationType.ColumnsNoSpacers:
                    return new ColumnListVisualizer(1, 30, 0, 1f);
                case VisualizationType.PositiveColumns:
                    return new PositiveColumnListVisualizer(1, 30, 5, 0.8f);
                case VisualizationType.PositiveColumnsNoSpacers:
                    return new PositiveColumnListVisualizer(1, 30, 0, 1f);
                case VisualizationType.Points:
                    return new PointsListVisualizer();
                default:
                    return null;
            }
        }
    }
}