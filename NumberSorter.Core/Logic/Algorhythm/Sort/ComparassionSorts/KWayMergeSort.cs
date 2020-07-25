using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.PositionLocator.Base;
using NumberSorter.Core.Logic.Factories.Sort.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class KWayMergeSort<T> : GenericSortAlgorhythm<T>
    {
        private int BatchSize { get; }

        private ISortRunLocator<T> SortRunLocator { get; }
        private ISortFactory RunSortFactory { get; }
        private IPositionLocatorFactory PositionLocatorFactory { get; }

        private T[] _buffer;
        private readonly Queue<SortRun> _sortRunsQueue;
        private readonly Queue<SortRun> _nextSortRunsQueue;

        public KWayMergeSort(IComparer<T> comparer, ISortFactory runSortFactory, ISortRunLocatorFactory sortRunLocatorFactory, IPositionLocatorFactory positionLocatorFactory, int batchSize) : base(comparer)
        {
            BatchSize = batchSize;
            _buffer = Array.Empty<T>();
            _sortRunsQueue = new Queue<SortRun>();
            _nextSortRunsQueue = new Queue<SortRun>();

            RunSortFactory = runSortFactory;
            PositionLocatorFactory = positionLocatorFactory;
            SortRunLocator = sortRunLocatorFactory.GetSortRunLocator(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            _buffer = new T[length];

            int index = startingIndex;
            int elementsLeft = length;
            while (elementsLeft != 0)
            {
                int elementsTaken = FindSortRuns(list, index, elementsLeft);
                index += elementsTaken;
                elementsLeft -= elementsTaken;
                var batch = GetSortRunBatch();
                SortBatch(list, batch);
            }

            while (_nextSortRunsQueue.Count > 1)
            {
                while (_nextSortRunsQueue.Count > 0)
                    _sortRunsQueue.Enqueue(_nextSortRunsQueue.Dequeue());
                while (_sortRunsQueue.Count > 0)
                {
                    var batch = GetSortRunBatch();
                    SortBatch(list, batch);
                }
            }
            _nextSortRunsQueue.Clear();


            //int bathSize = 2;
            //int batchCount = (int)Math.Ceiling(sortRuns.Count / (float)bathSize);
            //int sortRunsLeft = sortRuns.Count;
            //int currentBatchIndex = 0;
        }

        private SortRun[] GetSortRunBatch()
        {
            int nextRunBatch = Math.Min(BatchSize, _sortRunsQueue.Count);
            var batch = new SortRun[nextRunBatch];
            for (int i = 0; i < nextRunBatch; i++)
                batch[i] = _sortRunsQueue.Dequeue();
            return batch;
        }

        private void SortBatch(IList<T> list, SortRun[] sortRuns)
        {
            if (sortRuns.Length == 1)
            {
                _nextSortRunsQueue.Enqueue(sortRuns[0]);
                return;
            }

            int totalElementCount = sortRuns.Sum(x => x.Length);

            var startIndex = sortRuns[0].FirstIndex;
            var sortRunComparer = Comparer<SortRun>.Create((x, y) => Compare(list[x.FirstIndex], list[y.FirstIndex]));
            RunSortFactory.Sort(sortRuns, sortRunComparer);

            int lowRunIndex = 0;
            int runIndexLimit = sortRuns.Length;
            int lastRunIndex = runIndexLimit - 1;

            SortRun nextSortRun = sortRuns[lowRunIndex];

            int index = 0;
            var elementPositionLocator = PositionLocatorFactory.GetPositionLocator(Comparer);
            var sortRunPositionLocator = PositionLocatorFactory.GetPositionLocator(sortRunComparer);

            while (lowRunIndex != lastRunIndex)
            {
                SortRun lowSortRun = nextSortRun;

                int nextRunIndex = lowRunIndex + 1;
                nextSortRun = sortRuns[nextRunIndex];
                T nextLimit = list[nextSortRun.FirstIndex];

                int start = lowSortRun.FirstIndex;
                int end = elementPositionLocator.FindLastPosition(list, nextLimit, lowSortRun.FirstIndex, lowSortRun.Length);

                int elementsToCopy = end - start;
                ListUtility.Copy(list, start, _buffer, index, elementsToCopy);
                index += elementsToCopy;

                var newRun = new SortRun(lowSortRun.FirstIndex + elementsToCopy, lowSortRun.Length - elementsToCopy);
                sortRuns[lowRunIndex] = newRun;

                if (newRun.Length == 0)
                {
                    lowRunIndex++;
                    continue;
                }

                int searchAreaLength = runIndexLimit - nextRunIndex;
                int indexToInsert = sortRunPositionLocator.FindLastPosition(sortRuns, newRun, nextRunIndex, searchAreaLength) - 1;

                int newRunIndex = lowRunIndex;
                while (newRunIndex != indexToInsert)
                {
                    sortRuns.Swap(newRunIndex, newRunIndex + 1);
                    newRunIndex++;
                }
            }

            ListUtility.Copy(list, nextSortRun.FirstIndex, _buffer, index, nextSortRun.Length);

            ListUtility.Copy(_buffer, 0, list, startIndex, totalElementCount);
            _nextSortRunsQueue.Enqueue(new SortRun(startIndex, totalElementCount));
        }

        private int FindSortRuns(IList<T> list, int startingIndex, int length)
        {
            int currentIndex = startingIndex;
            int elementsLeft = length;
            int runsLeft = BatchSize;
            while (elementsLeft > 0 && runsLeft > 0)
            {
                var sortRun = SortRunLocator.FindNextSortRun(list, currentIndex, elementsLeft);
                _sortRunsQueue.Enqueue(sortRun);
                elementsLeft -= sortRun.Length;
                currentIndex += sortRun.Length;
                runsLeft--;
            }
            return length - elementsLeft;
        }
    }
}
