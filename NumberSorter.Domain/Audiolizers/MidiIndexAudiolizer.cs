using NAudio.Midi;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Logic;
using System;

namespace NumberSorter.Domain.Audiolizers
{
    public class MidiIndexAudiolizer : IStateAudiolizer
    {
        private int MinNote { get; }
        private int MaxNote { get; }
        private int NoteRange { get; }

        private int ElementCount { get; set; }

        private MidiOut MidiOut { get; }

        // Range of notes is from 0 to 127
        public MidiIndexAudiolizer(int minNote, int maxNote, MidiInstrumentType instrumentType)
        {
            MinNote = minNote;
            MaxNote = maxNote;

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
            ElementCount = sortLog.InputState.State.Count;
        }

        public void Play(SortState<int> sortState)
        {
            foreach (var value in sortState.HighlightedIndexes)
            {
                int note = (int)(value / (double)ElementCount * NoteRange) + MinNote;
                MidiOut.Send(new NoteEvent(0, 1, MidiCommandCode.NoteOn, note, 127 - (note / 2)).GetAsShortMessage());
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
