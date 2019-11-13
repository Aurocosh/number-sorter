using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class GapGeneratorTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public GapGeneratorType Type { get; }

        public GapGeneratorTypeLineViewModel(GapGeneratorType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
