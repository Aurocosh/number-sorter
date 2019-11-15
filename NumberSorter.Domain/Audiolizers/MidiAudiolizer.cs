using NAudio.Midi;
using NumberSorter.Core.Logic.Utility;
using NumberSorter.Domain.Audiolizers.Base;
using NumberSorter.Domain.Container;

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

        public MidiValueAudiolizer()
        {
            Minimum = 0;
            Maximum = 0;

            // Range of notes is from 0 to 127
            MinNote = 30;
            MaxNote = 120;
            NoteRange = MaxNote - MinNote;

            MidiOut = new MidiOut(0)
            {
                Volume = 65535
            };
            MidiOut.Send(MidiMessage.ChangePatch(4, 1).RawData);
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
                MidiOut.Send(new NoteEvent(0, 1, MidiCommandCode.NoteOn, note, 127 - note / 2).GetAsShortMessage());
            }
        }
    }
}
