using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class MSDRadixSort : IIntegerSortAlgorhythm
    {
        private int BucketCount { get; }

        public MSDRadixSort()
        {
            BucketCount = 4;
        }

        public MSDRadixSort(int bucketCount)
        {
            BucketCount = bucketCount;
        }

        public void Sort(IList<int> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<int> list, int startingIndex, int length)
        {
            int nextPositiveIndex = startingIndex + length - 1;
            int nextUnsortedIndex = startingIndex;
            int elementsLeft = length;
            while (elementsLeft-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                if (nextUnsorted < 0)
                    nextUnsortedIndex++;
                else
                    list.Swap(nextUnsortedIndex, nextPositiveIndex--);
            }
            int negativeLength = nextUnsortedIndex - startingIndex;
            int positiveLength = length - negativeLength;

            int positiveIndex = startingIndex + negativeLength;

            IntListUtility.InvertNumbers(list, startingIndex, negativeLength);

            int highestPower = FindMaxLog(list, startingIndex, negativeLength, BucketCount);
            Sort(list, startingIndex, negativeLength, BucketCount, highestPower);

            highestPower = FindMaxLog(list, positiveIndex, positiveLength, BucketCount);
            Sort(list, positiveIndex, positiveLength, BucketCount, highestPower);

            IntListUtility.InvertNumbers(list, startingIndex, negativeLength);
            ListUtility.InvertPart(list, startingIndex, negativeLength);
        }

        private void Sort(IList<int> array, int startingIndex, int length, int radix, int power)
        {
            int indexLimit = startingIndex + length;
            if (startingIndex >= indexLimit || power < 0)
                return;

            var buckets = new List<int>[radix];

            for (int i = 0; i < radix; i++)
                buckets[i] = new List<int>();

            for (int i = startingIndex; i < indexLimit; i++)
            {
                int digit = GetBucketIndex(array[i], power, radix);
                buckets[digit].Add(array[i]);
            }

            EmptyBuckets(array, buckets, startingIndex, length);

            int index = startingIndex;
            for (int i = 0; i < buckets.Length; i++)
            {
                ref var bucket = ref buckets[i];
                Sort(array, index, bucket.Count, radix, power - 1);
                index += bucket.Count;
                bucket.Clear();
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
                {
                    list[lastIndex--] = bucket[i];
                }
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
