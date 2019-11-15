using NumberSorter.Domain.Logic;
using ReactiveUI;

namespace NumberSorter.Domain.ViewModels
{
    public class MidiInstrumentTypeLineViewModel : ReactiveObject
    {
        public string Name { get; }
        public MidiInstrumentType Type { get; }

        public MidiInstrumentTypeLineViewModel(MidiInstrumentType type, string name)
        {
            Type = type;
            Name = name;
        }
    }
}
