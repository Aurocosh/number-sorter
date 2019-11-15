using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class MidiInstrumentNamer
    {
        private static readonly Dictionary<MidiInstrumentType, string> _nameDictionary = new Dictionary<MidiInstrumentType, string>();

        static MidiInstrumentNamer()
        {
            _nameDictionary.Add(MidiInstrumentType.AcousticGrandPiano, "Piano - Acoustic Grand Piano");
            _nameDictionary.Add(MidiInstrumentType.BrightAcousticPiano, "Piano - Bright Acoustic Piano");
            _nameDictionary.Add(MidiInstrumentType.ElectricGrandPiano, "Piano - Electric Grand Piano");
            _nameDictionary.Add(MidiInstrumentType.HonkyTonkPiano, "Piano - Honky-tonk Piano");
            _nameDictionary.Add(MidiInstrumentType.ElectricPiano1, "Piano - Electric Piano 1");
        }

        public static string GetName(MidiInstrumentType type)
        {
            return _nameDictionary[type];
        }
    }
}