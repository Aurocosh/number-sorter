using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class LSDRadixSort : IIntegerSortAlgorhythm
    {
        private int BucketCount { get; }
        private ISignSeparatorAlgothythm SignSeparator { get; }

        public LSDRadixSort(int bucketCount, ISignSeparatorAlgothythm signSeparator)
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

            int highestPower = FindMaxLog(list, startingIndex, negativeLength, BucketCount);
            Sort(list, startingIndex, negativeLength, BucketCount, highestPower);

            highestPower = FindMaxLog(list, positiveIndex, positiveLength, BucketCount);
            Sort(list, positiveIndex, positiveLength, BucketCount, highestPower);

            IntListUtility.InvertPartAndNumbers(list, startingIndex, negativeLength);
        }

        private void Sort(IList<int> array, int startingIndex, int length, int bucketCount, int maxPower)
        {
            if (maxPower < 0)
                return;

            var buckets = new List<int>[bucketCount];

            for (int index = 0; index < bucketCount; index++)
                buckets[index] = new List<int>();

            int indexLimit = startingIndex + length;
            for (int power = 0; power <= maxPower; power++)
            {
                for (int index = startingIndex; index < indexLimit; index++)
                {
                    int digit = GetBucketIndex(array[index], power, bucketCount);
                    buckets[digit].Add(array[index]);
                }

                EmptyBuckets(array, buckets, startingIndex, length);
            }
        }

        private int GetBucketIndex(int value, int power, int radix)
        {
            return (int)(value / Math.Pow(radix, power)) % radix;
        }

        private void EmptyBuckets(IList<int> list, List<int>[] buckets, int startingIndex, int length)
        {
            int lastIndex = startingIndex + length - 1;
            for (int index = buckets.Length - 1; index >= 0; index--)
            {
                ref var bucket = ref buckets[index];
                for (int i = bucket.Count - 1; i >= 0; i--)
                    list[lastIndex--] = bucket[i];
                bucket.Clear();
            }
        }

        private int FindMaxLog(IList<int> list, int startingIndex, int length, int baseValue)
        {
            int maxLog = 0;
            int indexLimit = startingIndex + length;
            for (int i = startingIndex; i != indexLimit; i++)
            {
                int log = (int)(Math.Log(list[i]) / Math.Log(baseValue));
                if (log > maxLog)
                    maxLog = log;
            }
            return maxLog;
        }
    }
}
