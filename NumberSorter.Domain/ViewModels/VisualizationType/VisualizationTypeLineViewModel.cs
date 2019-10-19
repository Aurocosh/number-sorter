using NumberSorter.Core.Logic;
using NumberSorter.Domain.Visualizers;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class VisualizationTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public VisualizationType VisualizationType { get; }

        public VisualizationTypeLineViewModel(VisualizationType visualizationType, string name)
        {
            VisualizationType = visualizationType;
            Name = name;
        }
    }
}
