using NumberSorter.Logic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.ViewModels
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
