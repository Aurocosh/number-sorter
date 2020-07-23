using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Factories.LocalMerge.Base;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class LinkedListStrandSort<T> : GenericSortAlgorhythm<T>
    {
        private ILocalMergeAlgothythm<T> LocalMergeAlgorhythm { get; }

        public LinkedListStrandSort(IComparer<T> comparer, ILocalMergeFactory localMergeFactory) : base(comparer)
        {
            LocalMergeAlgorhythm = localMergeFactory.GetLocalMerge(comparer);
        }

        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            var source = new LinkedList<T>();
            var buffer = new LinkedList<T>();

            int indexLimit = startingIndex + length;
            for (int i = startingIndex; i < indexLimit; i++)
                source.AddLast(list[i]);

            int resultSize = 0;
            int targetIndex = startingIndex;
            while (source.Count > 0)
            {
                T nextInBuffer = source.First.Value;
                buffer.AddLast(nextInBuffer);
                source.RemoveFirst();

                var currentNode = source.First;
                while (currentNode != null)
                {
                    var nextNode = currentNode.Next;
                    T nextInSource = currentNode.Value;
                    if (Compare(nextInSource, nextInBuffer) >= 0)
                    {
                        nextInBuffer = nextInSource;
                        buffer.AddLast(nextInSource);
                        source.Remove(currentNode);
                    }
                    currentNode = nextNode;
                }

                var leftRun = new SortRun(startingIndex, resultSize);
                var rightRun = new SortRun(targetIndex, buffer.Count);

                foreach (var value in buffer)
                    list[targetIndex++] = value;

                LocalMergeAlgorhythm.Merge(list, leftRun, rightRun);

                resultSize += buffer.Count;
                buffer.Clear();
            }
        }
    }
}
