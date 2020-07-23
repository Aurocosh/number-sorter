using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public sealed class ArrayRunMerger<T> : IRunMerger
    {
        private int _stackSize;
        private readonly IList<T> _list;
        private readonly SortRun[] _sortRuns;

        private readonly ILocalMergeAlgothythm<T> _localMergeAlgorhythm;

        public ArrayRunMerger(IList<T> list, ILocalMergeAlgothythm<T> localMergeAlgorhythm)
        {
            _list = list;
            _stackSize = 0;

            int stackMaxLength = (int)Math.Ceiling(Math.Log(list.Count, 2));
            _sortRuns = new SortRun[stackMaxLength];

            _localMergeAlgorhythm = localMergeAlgorhythm;
        }

        public void Push(SortRun sortRun)
        {
            _sortRuns[_stackSize++] = sortRun;

            while (_stackSize > 1)
            {
                var indexX = _stackSize - 1;
                var indexY = _stackSize - 2;
                var indexZ = _stackSize - 3;

                int lenX = _sortRuns[indexX].Length;
                int lenY = _sortRuns[indexY].Length;
                int lenZ = indexZ >= 0 ? _sortRuns[indexZ].Length : int.MaxValue;

                if (lenZ <= lenY + lenX || lenY <= lenX)
                {
                    if (lenZ < lenX)
                        MergeWithNextRun(indexZ);
                    else
                        MergeWithNextRun(indexY);
                }
                else
                {
                    return;
                }
            }
        }

        public void ForceMerge()
        {
            while (_stackSize > 1)
            {
                var indexX = _stackSize - 1;
                var indexZ = _stackSize - 3;

                int lenX = _sortRuns[indexX].Length;
                int lenZ = indexZ >= 0 ? _sortRuns[indexZ].Length : int.MaxValue;

                if (lenZ < lenX)
                    MergeWithNextRun(indexZ);
                else
                    MergeWithNextRun(indexZ + 1);
            }
        }

        public void MergeWithNextRun(int runIndex)
        {
            var leftRun = _sortRuns[runIndex];
            var rightRun = _sortRuns[runIndex + 1];

            _sortRuns[runIndex] = leftRun.Expand(rightRun.Length);
            if (runIndex == _stackSize - 3)
                _sortRuns[runIndex + 1] = _sortRuns[runIndex + 2];
            _stackSize--;

            _localMergeAlgorhythm.Merge(_list, leftRun, rightRun);
        }
    }
}