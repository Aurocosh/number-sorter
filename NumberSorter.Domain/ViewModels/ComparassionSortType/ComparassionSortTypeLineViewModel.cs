using NumberSorter.Domain.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class ComparassionSortTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public ComparassionAlgorhythmType Type { get; }

        public ComparassionSortTypeLineViewModel(ComparassionAlgorhythmType algorhythmType, String name)
        {
            Type = algorhythmType;
            Name = name;
        }
    }
}
