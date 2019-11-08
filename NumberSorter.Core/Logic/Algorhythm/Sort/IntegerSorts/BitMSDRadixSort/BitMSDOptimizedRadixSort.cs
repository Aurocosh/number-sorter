using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.SignSeparator.Base;
using NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator;
using NumberSorter.Core.Logic.Algorhythm.SortBitMaskGenerator.Base;
using NumberSorter.Core.Logic.Utility;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm.IntegerSort
{
    public class BitMSDOptimizedRadixSort : IIntegerSortAlgorhythm
    {
        private ISignSeparatorAlgothythm SignSeparator { get; }
        private ISortBitMaskGenerator SortBitMaskGenerator { get; }

        public BitMSDOptimizedRadixSort(ISignSeparatorAlgothythm signSeparator)
        {
            SignSeparator = signSeparator;
            SortBitMaskGenerator = new ZerosSortBitMaskGenerator();
        }

        public BitMSDOptimizedRadixSort(ISignSeparatorAlgothythm signSeparator, ISortBitMaskGenerator sortBitMaskGenerator) : this(signSeparator)
        {
            SortBitMaskGenerator = sortBitMaskGenerator;
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

            int bitMask = SortBitMaskGenerator.GenerateMask(list, startingIndex, negativeLength);
            Sort(list, startingIndex, negativeLength, 0, bitMask);

            bitMask = SortBitMaskGenerator.GenerateMask(list, positiveIndex, positiveLength);
            Sort(list, positiveIndex, positiveLength, 0, bitMask);

            IntListUtility.InvertPartAndNumbers(list, startingIndex, negativeLength);
        }

        public void Sort(IList<int> list, int startingIndex, int length, int shift, int mask)
        {
            if (length < 2)
                return;

            while (shift < 32 && (mask << shift) > 0)
                shift++;

            if (shift > 31)
                return;

            int nextBigIndex = startingIndex + length - 1;
            int nextUnsortedIndex = startingIndex;
            int unsortedElementCount = length;
            while (unsortedElementCount-- > 0)
            {
                var nextUnsorted = list[nextUnsortedIndex];
                bool move = (nextUnsorted << shift) >= 0;
                if (shift == 0 ? !move : move)
                    nextUnsortedIndex++;
                else
                    list.Swap(nextUnsortedIndex, nextBigIndex--);
            }
            int leftLength = nextUnsortedIndex - startingIndex;
            int rightLength = length - leftLength;

            Sort(list, startingIndex, leftLength, shift + 1, mask);
            Sort(list, startingIndex + leftLength, rightLength, shift + 1, mask);
        }
    }
}
