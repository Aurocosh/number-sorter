using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class PositionLocatorTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public PositionLocatorType Type { get; }

        public PositionLocatorTypeLineViewModel(PositionLocatorType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
