using NumberSorter.Core.Algorhythm;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    public class SmoothSort<T> : GenericSortAlgorhythm<T>, IPartialSortAlgorhythm<T>
    {
        private static readonly int[] _leonardoNumbers = {
            1, 1, 3, 5, 9, 15, 25, 41, 67, 109,
            177, 287, 465, 753, 1219, 1973, 3193, 5167, 8361, 13529,
            21891, 35421, 57313, 92735, 150049, 242785, 392835, 635621, 1028457, 1664079,
            2692537, 4356617, 7049155, 11405773, 18454929, 29860703, 48315633, 78176337, 126491971, 204668309,
            331160281, 535828591, 866988873
        };

        private static readonly int[] _lookup = {
            32, 0, 1, 26, 2, 23, 27, 0, 3, 16, 24, 30, 28, 11, 0, 13, 4, 7, 17,
            0, 25, 22, 31, 15, 29, 10, 12, 6, 0, 21, 14, 9, 5, 20, 8, 19, 18};

        private static int NumberOfTrailingZeros(int integer)
        {
            return _lookup[(integer & -integer) % 37];
        }

        public SmoothSort(IComparer<T> comparer) : base(comparer) { }

        public override void Sort(IList<T> list)
        {
            Sort(list, 0, list.Count);
        }

        public void Sort(IList<T> list, int startingIndex, int length)
        {
            int low = startingIndex;
            int high = startingIndex + length - 1;
            int head = low; // the offset of the first element of the prefix into m

            // These variables need a little explaining. If our string of heaps
            // is of length 38, then the heaps will be of size 25+9+3+1, which are
            // Leonardo numbers 6, 4, 2, 1.
            // Turning this into a binary number, we get b01010110 = 0x56. We represent
            // this number as a pair of numbers by right-shifting all the zeros and
            // storing the mantissa and exponent as "p" and "pshift".
            // This is handy, because the exponent is the index into L[] giving the
            // size of the rightmost heap, and because we can instantly find out if
            // the rightmost two heaps are consecutive Leonardo numbers by checking
            // (p&3)==3

            int p = 1; // the bitmap of the current standard concatenation >> pshift
            int pshift = 1;

            while (head < high)
            {
                if ((p & 3) == 3)
                {
                    // Add 1 by merging the first two blocks into a larger one.
                    // The next Leonardo number is one bigger.
                    Sift(list, pshift, head);
                    p >>= 2;
                    pshift += 2;
                }
                else
                {
                    // adding a new block of length 1
                    if (_leonardoNumbers[pshift - 1] >= high - head)
                    {
                        // this block is its final size.
                        Trinkle(list, p, pshift, head, false);
                    }
                    else
                    {
                        // this block will get merged. Just make it trusty.
                        Sift(list, pshift, head);
                    }

                    if (pshift == 1)
                    {
                        // LP[1] is being used, so we add use LP[0]
                        p <<= 1;
                        pshift--;
                    }
                    else
                    {
                        // shift out to position 1, add LP[1]
                        p <<= pshift - 1;
                        pshift = 1;
                    }
                }
                p |= 1;
                head++;
            }

            Trinkle(list, p, pshift, head, false);

            while (pshift != 1 || p != 1)
            {
                if (pshift <= 1)
                {
                    // block of length 1. No fiddling needed
                    int trail = NumberOfTrailingZeros(p & ~1);
                    p >>= trail;
                    pshift += trail;
                }
                else
                {
                    p <<= 2;
                    p ^= 7;
                    pshift -= 2;

                    // This block gets broken into three bits. The rightmost bit is a
                    // block of length 1. The left hand part is split into two, a block
                    // of length LP[pshift+1] and one of LP[pshift].  Both these two
                    // are appropriately heapified, but the root nodes are not
                    // necessarily in order. We therefore semitrinkle both of them

                    Trinkle(list, p >> 1, pshift + 1, head - _leonardoNumbers[pshift] - 1, true);
                    Trinkle(list, p, pshift, head - 1, true);
                }
                head--;
            }
        }

        private void Sift(IList<T> list, int pshift, int head)
        {
            // we do not use Floyd's improvements to the heapsort sift, because we
            // are not doing what heapsort does - always moving nodes from near
            // the bottom of the tree to the root.

            T value = list[head];

            while (pshift > 1)
            {
                int right = head - 1;
                int left = head - 1 - _leonardoNumbers[pshift - 2];

                if (Compare(value, list[left]) >= 0 && Compare(value, list[right]) >= 0)
                    break;

                if (Compare(list[left], list[right]) >= 0)
                {
                    list[head] = list[left];
                    head = left;
                    pshift--;
                }
                else
                {
                    list[head] = list[right];
                    head = right;
                    pshift -= 2;
                }
            }
            list[head] = value;
        }

        private void Trinkle(IList<T> list, int p, int pshift, int head, bool isTrusty)
        {
            T value = list[head];

            while (p != 1)
            {
                int stepson = head - _leonardoNumbers[pshift];

                if (Compare(list[stepson], value) <= 0)
                    break; // current node is greater than head. sift.

                // no need to check this if we know the current node is trusty,
                // because we just checked the head (which is val, in the first
                // iteration)
                if (!isTrusty && pshift > 1)
                {
                    int right = head - 1;
                    int left = head - 1 - _leonardoNumbers[pshift - 2];

                    if (Compare(list[right], list[stepson]) >= 0 || Compare(list[left], list[stepson]) >= 0)
                        break;
                }
                list[head] = list[stepson];

                head = stepson;
                int trail = NumberOfTrailingZeros(p & ~1);
                p >>= trail;
                pshift += trail;
                isTrusty = false;
            }

            if (!isTrusty)
            {
                list[head] = value;
                Sift(list, pshift, head);
            }
        }
    }
}
