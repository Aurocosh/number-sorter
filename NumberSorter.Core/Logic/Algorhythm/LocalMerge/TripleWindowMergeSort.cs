﻿using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    public class TripleWindowMerge<T> : GenericMergeAlgorhythm<T>
    {
        private readonly ILocalRotationAlgothythm<T> _localMergeAlgothythm;

        public TripleWindowMerge(IComparer<T> comparer) : base(comparer)
        {
            _localMergeAlgothythm = new RecursiveInPlaceRotation<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            //var first = SortRunUtility.RunToString(list, firstRun);
            //var second = SortRunUtility.RunToString(list, secondRun);

            //Console.WriteLine($"\n\nFirst ({firstRun.Start},{firstRun.Length}) Second ({secondRun.Start},{secondRun.Length})");
            //Console.WriteLine($"\nStart {first}   {second}");

            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            int firstIndex = firstRun.Start;
            int secondIndex = secondRun.Start;
            int lastSecondIndex = secondRun.LastIndex;

            int mainTempLength = 0;
            int firstTempLength = 0;
            int secondTempLength = 0;

            int firstSourceIndex = firstRun.Start;
            int mainTempStartIndex = secondRun.Start;

            while (firstIndex != secondIndex && mainTempStartIndex <= lastSecondIndex)
            {
                bool hasSecond = secondIndex <= lastSecondIndex;

                //var nextFromFirst = list[firstSourceIndex];
                //var nextFromSecond = hasSecond ? list[secondIndex].ToString() : "X";

                //first = SortRunUtility.RunToString(list, firstRun, firstIndex, secondIndex, firstSourceIndex);
                //second = SortRunUtility.RunToString(list, secondRun, firstIndex, secondIndex, firstSourceIndex);
                //var firstTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex, firstTempLength));
                //var mainTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex + firstTempLength, mainTempLength));
                //var secondTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex + firstTempLength + mainTempLength, secondTempLength));
                //Console.WriteLine($"\n\nBefore");
                //Console.WriteLine($"First {nextFromFirst} g({firstSourceIndex}) Second {nextFromSecond} g({secondIndex})");
                //Console.WriteLine($"{firstIndex} \n{first}\n{second}");
                //Console.WriteLine($"Temporary S({mainTempStartIndex}) L({firstTempLength}) V({firstTemp})");
                //Console.WriteLine($"Temporary S({mainTempStartIndex + firstTempLength}) L({mainTempLength}) V({mainTemp})");
                //Console.WriteLine($"Temporary S({mainTempStartIndex + firstTempLength + mainTempLength}) L({secondTempLength}) V({secondTemp})");

                if (hasSecond && Compare(list, firstSourceIndex, secondIndex) > 0)
                {
                    list.Swap(firstIndex, secondIndex);
                    if (firstTempLength == 0)
                        mainTempLength++;
                    else
                        secondTempLength++;

                    if (firstSourceIndex < mainTempStartIndex)
                        firstSourceIndex = mainTempStartIndex;
                    secondIndex++;
                }
                else if (firstIndex != firstSourceIndex)
                {
                    list.Swap(firstIndex, firstSourceIndex);
                    mainTempLength--;
                    firstTempLength++;
                    firstSourceIndex++;
                }
                else
                {
                    firstSourceIndex++;
                }

                firstIndex++;
                if (firstIndex == mainTempStartIndex)
                {
                    if (firstTempLength != 0 && mainTempLength != 0)
                        SortTemporary(list, mainTempStartIndex, firstTempLength + mainTempLength, firstSourceIndex);
                    if (firstTempLength != 0 && secondTempLength != 0)
                        Merge(list, new SortRun(mainTempStartIndex + mainTempLength, firstTempLength), new SortRun(mainTempStartIndex + mainTempLength + firstTempLength, secondTempLength));

                    mainTempLength = 0;
                    firstTempLength = 0;
                    secondTempLength = 0;
                    firstSourceIndex = mainTempStartIndex;
                    mainTempStartIndex = secondIndex;
                }
                else if (mainTempLength == 0 && firstTempLength + secondTempLength > 0)
                {
                    if (firstTempLength != 0 && secondTempLength != 0)
                        Merge(list, new SortRun(mainTempStartIndex, firstTempLength), new SortRun(mainTempStartIndex + firstTempLength, secondTempLength));
                    mainTempLength = firstTempLength + secondTempLength;
                    firstTempLength = 0;
                    secondTempLength = 0;
                    firstSourceIndex = mainTempStartIndex;
                }

                //first = SortRunUtility.RunToString(list, firstRun, firstIndex, secondIndex, firstSourceIndex);
                //second = SortRunUtility.RunToString(list, secondRun, firstIndex, secondIndex, firstSourceIndex);
                //firstTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex, firstTempLength));
                //mainTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex + firstTempLength, mainTempLength));
                //secondTemp = SortRunUtility.RunToString(list, new SortRun(mainTempStartIndex + firstTempLength + mainTempLength, secondTempLength));
                //Console.WriteLine($"\n\nAfter");
                //Console.WriteLine($"{first}\n{second}");
                //Console.WriteLine($"Temporary S({mainTempStartIndex}) L({firstTempLength}) V({firstTemp})");
                //Console.WriteLine($"Temporary S({mainTempStartIndex + firstTempLength}) L({mainTempLength}) V({mainTemp})");
                //Console.WriteLine($"Temporary S({mainTempStartIndex + firstTempLength + mainTempLength}) L({secondTempLength}) V({secondTemp})");
                //if (!IsSorted(list, firstRun.Start, firstRun.Start - firstIndex - 1))
                //    Console.WriteLine("Sort error");
            }

            //first = SortRunUtility.RunToString(list, firstRun);
            //second = SortRunUtility.RunToString(list, secondRun);
            //Console.WriteLine($"\nAfter {first}   {second}");
            //if (!IsSorted(list, firstRun.Start, firstRun.Length + secondRun.Length))
            //    Console.WriteLine("Not sorted");
        }

        private void SortTemporary(IList<T> list, int tempStartIndex, int tempLength, int firstSourceIndex)
        {
            var firstLength = firstSourceIndex - tempStartIndex;
            var secondLength = tempLength - firstLength;

            var leftRun = new SortRun(tempStartIndex, firstLength);
            var rightRun = new SortRun(firstSourceIndex, secondLength);

            _localMergeAlgothythm.Rotate(list, leftRun, rightRun);
        }
    }
}
