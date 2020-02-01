using NAudio.Midi;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Logic;
using System;

namespace NumberSorter.Domain.Audiolizers
{
    public class MidiValueIndexAudiolizer : IStateAudiolizer
    {
        private int MinNote { get; }
        private int MaxNote { get; }
        private int NoteRange { get; }

        private int Minimum { get; set; }
        private int Maximum { get; set; }
        private int Range { get; set; }
        private int ElementCount { get; set; }

        private MidiOut MidiOut { get; }

        // Range of notes is from 0 to 127
        public MidiValueIndexAudiolizer(int minNote, int maxNote, MidiInstrumentType instrumentType)
        {
            MinNote = minNote;
            MaxNote = maxNote;

            Minimum = 0;
            Maximum = 0;

            NoteRange = MaxNote - MinNote;
            ElementCount = 0;

            MidiOut = new MidiOut(0)
            {
                Volume = 65535
            };
            MidiOut.Send(MidiMessage.ChangePatch((int)instrumentType, 1).RawData);
        }

        public void Init(SortLog<int> sortLog)
        {
            var state = sortLog.InputState.State;
            Minimum = state.MinOrDefault();
            Maximum = state.MaxOrDefault();
            Range = Maximum - Minimum;
            ElementCount = state.Count;
        }

        public void Play(SortState<int> sortState)
        {
            var indexes = sortState.HighlightedIndexes;
            var values = sortState.HighlightedValues;

            int highlightCount = indexes.Count;
            for (int i = 0; i < highlightCount; i++)
            {
                var absValue = values[i] - Minimum;
                int note = (int)(absValue / (double)Range * NoteRange) + MinNote;
                int velocity = (int)(indexes[i] / (double)ElementCount * NoteRange) + MinNote;
                MidiOut.Send(new NoteOnEvent(0, 1, note, velocity, 1).GetAsShortMessage());
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                MidiOut.Dispose();
        }
    }
}
