using NumberSorter.Domain.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class PivotSelectorTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public PivotSelectorType Type { get; }

        public PivotSelectorTypeLineViewModel(PivotSelectorType pivotType, String name)
        {
            Type = pivotType;
            Name = name;
        }
    }
}
