using NumberSorter.Core.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class SortTypeLineViewModel : ReactiveObject
    {
        public string Description { get; }
        public AlgorhythmType AlgorhythmType { get; }

        public SortTypeLineViewModel(AlgorhythmType algorhythmType, String name)
        {
            AlgorhythmType = algorhythmType;
            Description = name;
        }
    }
}
