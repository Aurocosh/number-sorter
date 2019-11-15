using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class AudiolizerTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public AudiolizerType Type { get; }

        public AudiolizerTypeLineViewModel(AudiolizerType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
