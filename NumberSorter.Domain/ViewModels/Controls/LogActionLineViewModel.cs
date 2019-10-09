using NumberSorter.Core.Logic;
using ReactiveUI;
using System;

namespace NumberSorter.Domain.ViewModels
{
    public class LogActionLineViewModel : ReactiveObject
    {
        public int Index { get; }
        public string Description { get; }

        public LogActionLineViewModel(int index, string description)
        {
            Index = index;
            Description = description;
        }
    }
}
