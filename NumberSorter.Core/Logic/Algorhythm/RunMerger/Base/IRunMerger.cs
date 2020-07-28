namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base
{
    public interface IRunMerger
    {
        void Push(SortRun sortRun);
        void ForceMerge();
    }
}
