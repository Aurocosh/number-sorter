using NAudio.Midi;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;
using NumberSorter.Domain.Logic;
using System;

namespace NumberSorter.Domain.Audiolizers
{
    public class MidiValueAudiolizer : IStateAudiolizer
    {
        private int MinNote { get; }
        private int MaxNote { get; }
        private int NoteRange { get; }

        private int Minimum { get; set; }
        private int Maximum { get; set; }
        private int Range { get; set; }

        private MidiOut MidiOut { get; }

        // Range of notes is from 0 to 127
        public MidiValueAudiolizer(int minNote, int maxNote, MidiInstrumentType instrumentType)
        {
            MinNote = minNote;
            MaxNote = maxNote;

            Minimum = 0;
            Maximum = 0;

            NoteRange = MaxNote - MinNote;

            MidiOut = new MidiOut(0)
            {
                Volume = 65535
            };
            MidiOut.Send(MidiMessage.ChangePatch((int)instrumentType, 1).RawData);
        }

        public void Init(SortLog<int> sortLog)
        {
            Minimum = sortLog.InputState.State.MinOrDefault();
            Maximum = sortLog.InputState.State.MaxOrDefault();
            Range = Maximum - Minimum;
        }

        public void Play(SortState<int> sortState)
        {
            foreach (var value in sortState.HighlightedValues)
            {
                var absValue = value - Minimum;
                int note = (int)(absValue / (double)Range * NoteRange) + MinNote;
                MidiOut.Send(new NoteOnEvent(0, 1, note, 127 - (note / 2), 1).GetAsShortMessage());
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
