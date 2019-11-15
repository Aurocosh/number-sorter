using NAudio.Midi;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;

namespace NumberSorter.Domain.Audiolizers
{
    public class MidiIndexAudiolizer : IStateAudiolizer
    {
        private int MinNote { get; }
        private int MaxNote { get; }
        private int NoteRange { get; }

        private int ElementCount { get; set; }

        private MidiOut MidiOut { get; }

        public MidiIndexAudiolizer()
        {
            // Range of notes is from 0 to 127
            MinNote = 30;
            MaxNote = 120;
            NoteRange = MaxNote - MinNote;

            ElementCount = 0;

            MidiOut = new MidiOut(0)
            {
                Volume = 65535
            };
            MidiOut.Send(MidiMessage.ChangePatch(4, 1).RawData);
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
    }
}
