using NumberSorter.Core.Logic;
using NumberSorter.Domain.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class ComparassionSortTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public ComparassionAlgorhythmType AlgorhythmType { get; }

        public ComparassionSortTypeLineViewModel(ComparassionAlgorhythmType algorhythmType, String name)
        {
            AlgorhythmType = algorhythmType;
            Name = name;
        }
    }
}
