using NumberSorter.Domain.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class DistributionSortTypeLineViewModel : ReactiveObject
    {
        public string Description { get; }
        public DistributionAlgorhythmType AlgorhythmType { get; }

        public DistributionSortTypeLineViewModel(DistributionAlgorhythmType algorhythmType, String name)
        {
            AlgorhythmType = algorhythmType;
            Description = name;
        }
    }
}
