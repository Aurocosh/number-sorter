using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class RecursiveInPlaceRotation<T> : ILocalRotationAlgothythm<T>
    {
        public RecursiveInPlaceRotation()
        {
        }

        public void Rotate(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            if (leftRun.Length < rightRun.Length)
                MergeForward(list, leftRun, rightRun);
            else
                MergeBackward(list, leftRun, rightRun);
        }

        public void MergeForward(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            var firstIndex = leftRun.Start;
            var secondIndex = rightRun.Start;

            var firstLength = leftRun.Length;
            var fullLength = leftRun.Length + rightRun.Length;

            var simpleForwardMoves = ((fullLength / firstLength) - 1) * firstLength;
            var unmergedLeftoverLength = fullLength - simpleForwardMoves - firstLength;

            //var temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nTemp \n{temp}");

            while (simpleForwardMoves-- > 0)
                list.Swap(firstIndex++, secondIndex++);

            if (unmergedLeftoverLength > 0)
            {
                var leftoverLeftRun = new SortRun(firstIndex, firstLength);
                var leftoverRightRun = new SortRun(firstIndex + firstLength, unmergedLeftoverLength);
                MergeBackward(list, leftoverLeftRun, leftoverRightRun);
            }

            //var stemp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nSorted Temp \n{stemp}");
            //if (!IsSorted(list, tempStartIndex, tempLength))
            //    Console.WriteLine("Temp not sorted");
        }

        public void MergeBackward(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            var firstIndex = leftRun.Start + leftRun.Length - 1;
            var secondIndex = firstIndex + rightRun.Length;

            var secondLength = rightRun.Length;
            var fullLength = leftRun.Length + rightRun.Length;

            var simpleBackwardMoves = ((fullLength / secondLength) - 1) * secondLength;
            var unmergedLeftoverLength = fullLength - simpleBackwardMoves - secondLength;

            //var temp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nTemp \n{temp}");

            while (simpleBackwardMoves-- > 0)
                list.Swap(firstIndex--, secondIndex--);

            if (unmergedLeftoverLength > 0)
            {
                var leftoverLeftRun = new SortRun(firstIndex + 1 - unmergedLeftoverLength, unmergedLeftoverLength);
                var leftoverRightRun = new SortRun(firstIndex + 1, secondLength);
                MergeForward(list, leftoverLeftRun, leftoverRightRun);
            }

            //var stemp = SortRunUtility.RunToString(list, new SortRun(tempStartIndex, tempLength));
            //Console.WriteLine($"\nSorted Temp \n{stemp}");
            //if (!IsSorted(list, tempStartIndex, tempLength))
            //    Console.WriteLine("Temp not sorted");
        }
    }
}
