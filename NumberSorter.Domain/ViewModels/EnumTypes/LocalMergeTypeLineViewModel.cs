using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class LocalMergeTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public LocalMergeType Type { get; }

        public LocalMergeTypeLineViewModel(LocalMergeType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
