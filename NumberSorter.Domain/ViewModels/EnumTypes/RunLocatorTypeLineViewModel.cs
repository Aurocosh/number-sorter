using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class RunLocatorTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public RunLocatorType Type { get; }

        public RunLocatorTypeLineViewModel(RunLocatorType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
