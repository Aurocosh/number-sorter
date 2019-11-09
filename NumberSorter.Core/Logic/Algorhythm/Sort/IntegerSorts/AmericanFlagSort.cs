using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class AmericanFlagSort : IIntegerSortAlgorhythm
    {
        private int BucketCount { get; }
        private ISignSeparatorAlgothythm SignSeparator { get; }

        public AmericanFlagSort(int bucketCount, ISignSeparatorAlgothythm signSeparator)
        {
            BucketCount = bucketCount;
            SignSeparator = signSeparator;
        }

        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            int negativeLength = SignSeparator.Separate(list, startingIndex, length);
            int positiveLength = length - negativeLength;
            int positiveIndex = startingIndex + negativeLength;

            IntListUtility.InvertNumbers(list, startingIndex, negativeLength);

            int divisor = GetDivisor(list, startingIndex, negativeLength);
            Sort(list, startingIndex, negativeLength, divisor);

            divisor = GetDivisor(list, positiveIndex, positiveLength);
            Sort(list, positiveIndex, positiveLength, divisor);

            IntListUtility.InvertPartAndNumbers(list, startingIndex, negativeLength);
        }

        private int GetDivisor(IList<int> list, int startingIndex, int length)
        {
            int maxLog = IntListUtility.FindMaxLog(list, startingIndex, length, BucketCount);
            int divisor = 1;
            for (int i = 0; i < maxLog; i++)
                divisor *= BucketCount;
            return divisor;
        }

        private int GetBucketIndex(int integer, int divisor)
        {
            return integer / divisor % BucketCount;
        }

        private void Sort(IList<int> list, int startingIndex, int length, int divisor)
        {
            int[] counts = new int[BucketCount];
            int[] offset = new int[BucketCount];

            int indexLimit = startingIndex + length;
            for (int index = startingIndex; index < indexLimit; index++)
            {
                int bucketIndex = GetBucketIndex(list[index], divisor);
                counts[bucketIndex]++;
            }

            offset[0] = startingIndex;
            for (int previousIndex = 0, index = 1; index < BucketCount; previousIndex++, index++)
                offset[index] = counts[previousIndex] + offset[previousIndex];

            for (int bucketIndex = 0; bucketIndex < BucketCount; bucketIndex++)
            {
                while (counts[bucketIndex] > 0)
                {
                    int originIndex = offset[bucketIndex];
                    int fromIndex = originIndex;
                    int value = list[fromIndex];

                    do
                    {
                        int toBucketIndex = GetBucketIndex(value, divisor);
                        int toIndex = offset[toBucketIndex];

                        counts[toBucketIndex]--;
                        offset[toBucketIndex]++;

                        int nextValue = list[toIndex];
                        list[toIndex] = value;

                        value = nextValue;
                        fromIndex = toIndex;
                    } while (fromIndex != originIndex);
                }
            }
            if (divisor > 1)
            {
                for (int bucketIndex = 0; bucketIndex < BucketCount; bucketIndex++)
                {
                    int bucketStart = (bucketIndex > 0) ? offset[bucketIndex - 1] : startingIndex;
                    int bucketLimit = offset[bucketIndex];
                    int bucketLength = bucketLimit - bucketStart;

                    if (bucketLength > 1)
                        Sort(list, bucketStart, bucketLength, divisor / BucketCount);
                }
            }
        }
    }
}
