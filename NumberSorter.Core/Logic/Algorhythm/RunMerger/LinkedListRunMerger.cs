using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public sealed class LinkedListRunMerger<T> : IRunMerger
    {
        private readonly LinkedList<SortRun> _sortRuns;

        private SortRun _sortRunX;
        private SortRun _sortRunY;
        private SortRun _sortRunZ;

        private readonly IList<T> _list;
        private readonly ILocalMergeAlgothythm<T> _localMergeAlgorhythm;

        public LinkedListRunMerger(IList<T> list, ILocalMergeAlgothythm<T> localMergeAlgorhythm)
        {
            _list = list;
            _localMergeAlgorhythm = localMergeAlgorhythm;

            _sortRuns = new LinkedList<SortRun>();

            _sortRunX = new SortRun(0, 0);
            _sortRunY = new SortRun(0, 0);
            _sortRunZ = new SortRun(0, 0);
        }

        public int Count => _sortRuns.Count;

        public void Push(SortRun sortRun)
        {
            _sortRuns.AddLast(_sortRunZ);
            _sortRunZ = _sortRunY;
            _sortRunY = _sortRunX;
            _sortRunX = sortRun;

            while (Count > 1)
            {
                int lenX = _sortRunX.Length;
                int lenY = _sortRunY.Length;
                int lenZ = Count >= 3 ? _sortRunZ.Length : int.MaxValue;

                if (lenZ <= lenY + lenX || lenY <= lenX)
                {
                    if (lenZ < lenX)
                    {
                        var X = Pop();
                        var Y = Pop();
                        var Z = Pop();

                        var mergedRun = MergeRuns(Z, Y);
                        Push(mergedRun);
                        Push(X);
                    }
                    else
                    {
                        var X = Pop();
                        var Y = Pop();

                        var mergedRun = MergeRuns(Y, X);
                        Push(mergedRun);
                    }
                }
                else
                {
                    return;
                }
            }
        }

        public SortRun Pop()
        {
            if (_sortRuns.Count == 0)
                return new SortRun(0, 0);
            var sortRun = _sortRunX;
            _sortRunX = _sortRunY;
            _sortRunY = _sortRunZ;
            _sortRunZ = _sortRuns.Last.Value;
            _sortRuns.RemoveLast();
            return sortRun;
        }

        public void ForceMerge()
        {
            while (Count > 1)
            {
                int lenX = _sortRunX.Length;
                int lenZ = Count >= 3 ? _sortRunZ.Length : int.MaxValue;

                if (lenZ < lenX)
                {
                    var X = Pop();
                    var Y = Pop();
                    var Z = Pop();

                    var mergedRun = MergeRuns(Z, Y);
                    Push(mergedRun);
                    Push(X);
                }
                else
                {
                    var X = Pop();
                    var Y = Pop();

                    var mergedRun = MergeRuns(Y, X);
                    Push(mergedRun);
                }
            }
        }

        private SortRun MergeRuns(SortRun leftRun, SortRun rightRun)
        {
            if (leftRun.FirstIndex > rightRun.FirstIndex)
            {
                var temp = leftRun;
                leftRun = rightRun;
                rightRun = temp;
            }
            _localMergeAlgorhythm.Merge(_list, leftRun, rightRun);
            return new SortRun(leftRun.Start, leftRun.Length + rightRun.Length);
        }
    }
}