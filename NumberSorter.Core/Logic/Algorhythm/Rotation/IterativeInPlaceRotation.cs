using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class IterativeInPlaceRotation<T> : ILocalRotationAlgothythm<T>
    {
        public IterativeInPlaceRotation()
        {
        }

        public void Rotate(IList<T> list, SortRun leftRun, SortRun rightRun)
        {
            while (leftRun.Length > 0 && rightRun.Length > 0)
            {
                if (leftRun.Length < rightRun.Length)
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

                    leftRun = new SortRun(firstIndex, firstLength);
                    rightRun = new SortRun(firstIndex + firstLength, unmergedLeftoverLength);
                }
                else
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

                    leftRun = new SortRun(firstIndex + 1 - unmergedLeftoverLength, unmergedLeftoverLength);
                    rightRun = new SortRun(firstIndex + 1, secondLength);
                }
            }
        }
    }
}
