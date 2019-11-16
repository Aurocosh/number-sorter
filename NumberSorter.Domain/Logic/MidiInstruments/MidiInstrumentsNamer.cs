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
            _nameDictionary.Add(MidiInstrumentType.ElectricPiano2, "Piano - Electric Piano 2");
            _nameDictionary.Add(MidiInstrumentType.Harpsichord, "Piano - Harpsichord");
            _nameDictionary.Add(MidiInstrumentType.Clavi, "Piano - Clavi");

            _nameDictionary.Add(MidiInstrumentType.Celesta, "Chromatic Percussion - Celesta");
            _nameDictionary.Add(MidiInstrumentType.Glockenspiel, "Chromatic Percussion - Glockenspiel");
            _nameDictionary.Add(MidiInstrumentType.MusicBox, "Chromatic Percussion - Music Box");
            _nameDictionary.Add(MidiInstrumentType.Vibraphone, "Chromatic Percussion - Vibraphone");
            _nameDictionary.Add(MidiInstrumentType.Marimba, "Chromatic Percussion - Marimba");
            _nameDictionary.Add(MidiInstrumentType.Xylophone, "Chromatic Percussion - Xylophone");
            _nameDictionary.Add(MidiInstrumentType.TubularBells, "Chromatic Percussion - Tubular Bells");
            _nameDictionary.Add(MidiInstrumentType.Dulcimer, "Chromatic Percussion - Dulcimer");

            _nameDictionary.Add(MidiInstrumentType.DrawbarOrgan, "Organ - Drawbar Organ");
            _nameDictionary.Add(MidiInstrumentType.PercussiveOrgan, "Organ - Percussive Organ");
            _nameDictionary.Add(MidiInstrumentType.RockOrgan, "Organ - Rock Organ");
            _nameDictionary.Add(MidiInstrumentType.ChurchOrgan, "Organ - Church Organ");
            _nameDictionary.Add(MidiInstrumentType.ReedOrgan, "Organ - Reed Organ");
            _nameDictionary.Add(MidiInstrumentType.Accordion, "Organ - Accordion");
            _nameDictionary.Add(MidiInstrumentType.Harmonica, "Organ - Harmonica");
            _nameDictionary.Add(MidiInstrumentType.TangoAccordion, "Organ - Tango Accordion");

            _nameDictionary.Add(MidiInstrumentType.AcousticGuitarNylon, "Guitar - Acoustic Guitar (nylon)");
            _nameDictionary.Add(MidiInstrumentType.AcousticGuitarSteel, "Guitar - Acoustic Guitar (steel)");
            _nameDictionary.Add(MidiInstrumentType.ElectricGuitarJazz, "Guitar - Electric Guitar (jazz)");
            _nameDictionary.Add(MidiInstrumentType.ElectricGuitarClean, "Guitar - Electric Guitar (clean)");
            _nameDictionary.Add(MidiInstrumentType.ElectricGuitarMuted, "Guitar - Electric Guitar (muted)");
            _nameDictionary.Add(MidiInstrumentType.OverdrivenGuitar, "Guitar - Overdriven Guitar");
            _nameDictionary.Add(MidiInstrumentType.DistortionGuitar, "Guitar - Distortion Guitar");
            _nameDictionary.Add(MidiInstrumentType.Guitarharmonics, "Guitar - Guitar harmonics");

            _nameDictionary.Add(MidiInstrumentType.AcousticBass, "Bass - Acoustic Bass");
            _nameDictionary.Add(MidiInstrumentType.ElectricBassFinger, "Bass - Electric Bass (finger)");
            _nameDictionary.Add(MidiInstrumentType.ElectricBassPick, "Bass - Electric Bass (pick)");
            _nameDictionary.Add(MidiInstrumentType.FretlessBass, "Bass - Fretless Bass");
            _nameDictionary.Add(MidiInstrumentType.SlapBass1, "Bass - Slap Bass 1");
            _nameDictionary.Add(MidiInstrumentType.SlapBass2, "Bass - Slap Bass 2");
            _nameDictionary.Add(MidiInstrumentType.SynthBass1, "Bass - Synth Bass 1");
            _nameDictionary.Add(MidiInstrumentType.SynthBass2, "Bass - Synth Bass 2");

            _nameDictionary.Add(MidiInstrumentType.Violin, "Strings - Violin");
            _nameDictionary.Add(MidiInstrumentType.Viola, "Strings - Viola");
            _nameDictionary.Add(MidiInstrumentType.Cello, "Strings - Cello");
            _nameDictionary.Add(MidiInstrumentType.Contrabass, "Strings - Contrabass");
            _nameDictionary.Add(MidiInstrumentType.TremoloStrings, "Strings - Tremolo Strings");
            _nameDictionary.Add(MidiInstrumentType.PizzicatoStrings, "Strings - Pizzicato Strings");
            _nameDictionary.Add(MidiInstrumentType.OrchestralHarp, "Strings - Orchestral Harp");
            _nameDictionary.Add(MidiInstrumentType.Timpani, "Strings - Timpani");

            _nameDictionary.Add(MidiInstrumentType.StringEnsemble1, "Ensemble - String Ensemble 1");
            _nameDictionary.Add(MidiInstrumentType.StringEnsemble2, "Ensemble - String Ensemble 2");
            _nameDictionary.Add(MidiInstrumentType.SynthStrings1, "Ensemble - SynthStrings 1");
            _nameDictionary.Add(MidiInstrumentType.SynthStrings2, "Ensemble - SynthStrings 2");
            _nameDictionary.Add(MidiInstrumentType.ChoirAahs, "Ensemble - Choir Aahs");
            _nameDictionary.Add(MidiInstrumentType.VoiceOohs, "Ensemble - Voice Oohs");
            _nameDictionary.Add(MidiInstrumentType.SynthVoice, "Ensemble - Synth Voice");
            _nameDictionary.Add(MidiInstrumentType.OrchestraHit, "Ensemble - Orchestra Hit");

            _nameDictionary.Add(MidiInstrumentType.Trumpet, "Brass - Trumpet");
            _nameDictionary.Add(MidiInstrumentType.Trombone, "Brass - Trombone");
            _nameDictionary.Add(MidiInstrumentType.Tuba, "Brass - Tuba");
            _nameDictionary.Add(MidiInstrumentType.MutedTrumpet, "Brass - Muted Trumpet");
            _nameDictionary.Add(MidiInstrumentType.FrenchHorn, "Brass - French Horn");
            _nameDictionary.Add(MidiInstrumentType.BrassSection, "Brass - Brass Section");
            _nameDictionary.Add(MidiInstrumentType.SynthBrass1, "Brass - SynthBrass 1");
            _nameDictionary.Add(MidiInstrumentType.SynthBrass2, "Brass - SynthBrass 2");

            _nameDictionary.Add(MidiInstrumentType.SopranoSax, "Reed - Soprano Sax");
            _nameDictionary.Add(MidiInstrumentType.AltoSax, "Reed - Alto Sax");
            _nameDictionary.Add(MidiInstrumentType.TenorSax, "Reed - Tenor Sax");
            _nameDictionary.Add(MidiInstrumentType.BaritoneSax, "Reed - Baritone Sax");
            _nameDictionary.Add(MidiInstrumentType.Oboe, "Reed - Oboe");
            _nameDictionary.Add(MidiInstrumentType.EnglishHorn, "Reed - English Horn");
            _nameDictionary.Add(MidiInstrumentType.Bassoon, "Reed - Bassoon");
            _nameDictionary.Add(MidiInstrumentType.Clarinet, "Reed - Clarinet");

            _nameDictionary.Add(MidiInstrumentType.Piccolo, "Pipe - Piccolo");
            _nameDictionary.Add(MidiInstrumentType.Flute, "Pipe - Flute");
            _nameDictionary.Add(MidiInstrumentType.Recorder, "Pipe - Recorder");
            _nameDictionary.Add(MidiInstrumentType.PanFlute, "Pipe - Pan Flute");
            _nameDictionary.Add(MidiInstrumentType.BlownBottle, "Pipe - Blown Bottle");
            _nameDictionary.Add(MidiInstrumentType.Shakuhachi, "Pipe - Shakuhachi");
            _nameDictionary.Add(MidiInstrumentType.Whistle, "Pipe - Whistle");
            _nameDictionary.Add(MidiInstrumentType.Ocarina, "Pipe - Ocarina");

            _nameDictionary.Add(MidiInstrumentType.Lead1Square, "Synth Lead - Lead 1 (square)");
            _nameDictionary.Add(MidiInstrumentType.Lead2Sawtooth, "Synth Lead - Lead 2 (sawtooth)");
            _nameDictionary.Add(MidiInstrumentType.Lead3Calliope, "Synth Lead - Lead 3 (calliope)");
            _nameDictionary.Add(MidiInstrumentType.Lead4Chiff, "Synth Lead - Lead 4 (chiff)");
            _nameDictionary.Add(MidiInstrumentType.Lead5Charang, "Synth Lead - Lead 5 (charang)");
            _nameDictionary.Add(MidiInstrumentType.Lead6Voice, "Synth Lead - Lead 6 (voice)");
            _nameDictionary.Add(MidiInstrumentType.Lead7Fifths, "Synth Lead - Lead 7 (fifths)");
            _nameDictionary.Add(MidiInstrumentType.Lead8BassLead, "Synth Lead - Lead 8 (bass + lead)");

            _nameDictionary.Add(MidiInstrumentType.Pad1Newage, "Synth Pad - Pad 1 (new age)");
            _nameDictionary.Add(MidiInstrumentType.Pad2Warm, "Synth Pad - Pad 2 (warm)");
            _nameDictionary.Add(MidiInstrumentType.Pad3Polysynth, "Synth Pad - Pad 3 (polysynth)");
            _nameDictionary.Add(MidiInstrumentType.Pad4Choir, "Synth Pad - Pad 4 (choir)");
            _nameDictionary.Add(MidiInstrumentType.Pad5Bowed, "Synth Pad - Pad 5 (bowed)");
            _nameDictionary.Add(MidiInstrumentType.Pad6Metallic, "Synth Pad - Pad 6 (metallic)");
            _nameDictionary.Add(MidiInstrumentType.Pad7Halo, "Synth Pad - Pad 7 (halo)");
            _nameDictionary.Add(MidiInstrumentType.Pad8Sweep, "Synth Pad - Pad 8 (sweep)");

            _nameDictionary.Add(MidiInstrumentType.FX1Rain, "Synth Effects - FX 1 (rain)");
            _nameDictionary.Add(MidiInstrumentType.FX2Soundtrack, "Synth Effects - FX 2 (soundtrack)");
            _nameDictionary.Add(MidiInstrumentType.FX3Crystal, "Synth Effects - FX 3 (crystal)");
            _nameDictionary.Add(MidiInstrumentType.FX4Atmosphere, "Synth Effects - FX 4 (atmosphere)");
            _nameDictionary.Add(MidiInstrumentType.FX5Brightness, "Synth Effects - FX 5 (brightness)");
            _nameDictionary.Add(MidiInstrumentType.FX6Goblins, "Synth Effects - FX 6 (goblins)");
            _nameDictionary.Add(MidiInstrumentType.FX7Echoes, "Synth Effects - FX 7 (echoes)");
            _nameDictionary.Add(MidiInstrumentType.FX8SciFi, "Synth Effects - FX 8 (sci-fi)");

            _nameDictionary.Add(MidiInstrumentType.Sitar, "Ethnic - Sitar");
            _nameDictionary.Add(MidiInstrumentType.Banjo, "Ethnic - Banjo");
            _nameDictionary.Add(MidiInstrumentType.Shamisen, "Ethnic - Shamisen");
            _nameDictionary.Add(MidiInstrumentType.Koto, "Ethnic - Koto");
            _nameDictionary.Add(MidiInstrumentType.Kalimba, "Ethnic - Kalimba");
            _nameDictionary.Add(MidiInstrumentType.Bagpipe, "Ethnic - Bag pipe");
            _nameDictionary.Add(MidiInstrumentType.Fiddle, "Ethnic - Fiddle");
            _nameDictionary.Add(MidiInstrumentType.Shanai, "Ethnic - Shanai");

            _nameDictionary.Add(MidiInstrumentType.TinkleBell, "Percussive - Tinkle Bell");
            _nameDictionary.Add(MidiInstrumentType.Agogo, "Percussive - Agogo");
            _nameDictionary.Add(MidiInstrumentType.SteelDrums, "Percussive - Steel Drums");
            _nameDictionary.Add(MidiInstrumentType.Woodblock, "Percussive - Woodblock");
            _nameDictionary.Add(MidiInstrumentType.TaikoDrum, "Percussive - Taiko Drum");
            _nameDictionary.Add(MidiInstrumentType.MelodicTom, "Percussive - Melodic Tom");
            _nameDictionary.Add(MidiInstrumentType.SynthDrum, "Percussive - Synth Drum");
            _nameDictionary.Add(MidiInstrumentType.ReverseCymbal, "Percussive - Reverse Cymbal");

            _nameDictionary.Add(MidiInstrumentType.GuitarFretNoise, "Sound effects - Guitar Fret Noise");
            _nameDictionary.Add(MidiInstrumentType.BreathNoise, "Sound effects - Breath Noise");
            _nameDictionary.Add(MidiInstrumentType.Seashore, "Sound effects - Seashore");
            _nameDictionary.Add(MidiInstrumentType.BirdTweet, "Sound effects - Bird Tweet");
            _nameDictionary.Add(MidiInstrumentType.TelephoneRing, "Sound effects - Telephone Ring");
            _nameDictionary.Add(MidiInstrumentType.Helicopter, "Sound effects - Helicopter");
            _nameDictionary.Add(MidiInstrumentType.Applause, "Sound effects - Applause");
            _nameDictionary.Add(MidiInstrumentType.Gunshot, "Sound effects - Gunshot");
        }

        public static string GetName(MidiInstrumentType type)
        {
            return _nameDictionary[type];
        }
    }
}