using System.Collections.Generic;

namespace NumberSorter.Domain.Logic
{
    public static class AudiolizerNamer
    {
        private static readonly Dictionary<AudiolizerType, string> _nameDictionary = new Dictionary<AudiolizerType, string>();

        static AudiolizerNamer()
        {
            _nameDictionary.Add(AudiolizerType.DummyAudiolizer, "Dummy audiolizer (No sound)");
            _nameDictionary.Add(AudiolizerType.MidiValueAudiolizer, "Midi value audiolizer");
            _nameDictionary.Add(AudiolizerType.MidiValueAudiolizerCustom, "Midi value audiolizer (Custom)");
            _nameDictionary.Add(AudiolizerType.MidiIndexAudiolizer, "Midi index audiolizer");
            _nameDictionary.Add(AudiolizerType.MidiIndexAudiolizerCustom, "Midi index audiolizer (Custom)");
        }

        public static string GetName(AudiolizerType algorhythmType) => _nameDictionary[algorhythmType];
    }
}