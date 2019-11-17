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
                    return new ColumnListVisualizer(4, 1, 0.8f);
                case VisualizationType.ColumnsNoSpacers:
                    return new ColumnListVisualizer(5, 0, 0.8f);
                case VisualizationType.GhostlyColumns:
                    return new GhostlyColumnListVisualizer(4, 1, 0.8f);
                case VisualizationType.GhostlyColumnsNoSpacers:
                    return new GhostlyColumnListVisualizer(5, 0, 0.8f);
                case VisualizationType.PositiveColumns:
                    return new PositiveColumnListVisualizer(4, 1, 0.8f);
                case VisualizationType.PositiveColumnsNoSpacers:
                    return new PositiveColumnListVisualizer(5, 0, 0.8f);
                case VisualizationType.Points:
                    return new PointsListVisualizer(5, 0, 0.9f);
                case VisualizationType.Squares:
                    return new SquaresListVisualizer(5, 0, 0.9f);
                default:
                    return null;
            }
        }
    }
}