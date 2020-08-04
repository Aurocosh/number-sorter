using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;

namespace NumberSorter.Core.Logic.Algorhythm
{
    // Port of the WikiSort written in Java from: https://github.com/BonzaiThePenguin/WikiSort
    public class WikiSort<T> : GenericSortAlgorhythm<T>
    {
        // structure to represent ranges within the list
        private sealed class Range
        {
            public int start;
            public int end;

            public Range(int start1, int end1)
            {
                start = start1;
                end = end1;
            }

            public Range()
            {
                start = 0;
                end = 0;
            }

            public void Set(int start1, int end1)
            {
                start = start1;
                end = end1;
            }

            public int Length()
            {
                return end - start;
            }

            public override string ToString()
            {
                return $"Start: {start} End: {end}";
            }
        }

        private sealed class Pull
        {
            public int from;
            public int to;
            public int count;

            public Range range;
            public Pull() { range = new Range(0, 0); }

            public void Reset()
            {
                range.Set(0, 0);
                from = 0;
                to = 0;
                count = 0;
            }
        }

        // calculate how to scale the index value to the range within the list
        // the bottom-up merge sort only operates on values that are powers of two,
        // so scale down to that power of two, then use a fraction to scale back again
        private sealed class Iterator
        {
            public int startingIndex;
            public int indexLimit;
            public int totalLength;
            public int powerOfTwo;

            public int numerator;
            public int integerPart;

            public int denominator;
            public int integerStep;
            public int numeratorStep;

            // 63 -> 32, 64 -> 64, etc.
            // this comes from Hacker's Delight
            private static int FloorPowerOfTwo(int value)
            {
                int x = value;
                x |= x >> 1;
                x |= x >> 2;
                x |= x >> 4;
                x |= x >> 8;
                x |= x >> 16;
                return x - (x >> 1);
            }

            public Iterator(int start, int length, int minLevel)
            {
                startingIndex = start;
                indexLimit = start + length;
                totalLength = length;
                powerOfTwo = FloorPowerOfTwo(totalLength);
                denominator = powerOfTwo / minLevel;
                numeratorStep = totalLength % denominator;
                integerStep = totalLength / denominator;
                Begin();
            }

            public void Begin()
            {
                numerator = 0;
                integerPart = startingIndex;
            }

            public Range NextRange()
            {
                int start = integerPart;

                integerPart += integerStep;
                numerator += numeratorStep;
                if (numerator >= denominator)
                {
                    numerator -= denominator;
                    integerPart++;
                }

                return new Range(start, integerPart);
            }

            public bool Finished()
            {
                return integerPart >= indexLimit;
            }

            public bool NextLevel()
            {
                integerStep += integerStep;
                numeratorStep += numeratorStep;
                if (numeratorStep >= denominator)
                {
                    numeratorStep -= denominator;
                    integerStep++;
                }

                return integerStep < totalLength;
            }

            public int Length()
            {
                return integerStep;
            }
        }

        private readonly ISortAlgorhythm<T> _minrunSort;

        //private static int _cacheSize = 512;
        private readonly int _cacheSize;
        private readonly T[] _cache;

        public WikiSort(IComparer<T> comparer, int cacheSize) : base(comparer)
        {
            _cacheSize = cacheSize;
            var cache1 = new T[_cacheSize];
            if (cache1 == null)
                _cacheSize = 0;
            else
                _cache = cache1;

            _minrunSort = new InsertionSort<T>(comparer);
        }

        // find the index of the first value within the range that is equal to list[index]
        private int BinaryFirst(IList<T> list, T value, Range range)
        {
            int start = range.start, end = range.end - 1;
            while (start < end)
            {
                int mid = start + (end - start) / 2;
                if (Compare(list[mid], value) < 0)
                    start = mid + 1;
                else
                    end = mid;
            }
            if (start == range.end - 1 && Compare(list[start], value) < 0) start++;
            return start;
        }

        // find the index of the last value within the range that is equal to list[index], plus 1
        private int BinaryLast(IList<T> list, T value, Range range)
        {
            int start = range.start, end = range.end - 1;
            while (start < end)
            {
                int mid = start + (end - start) / 2;
                if (Compare(value, list[mid]) >= 0)
                    start = mid + 1;
                else
                    end = mid;
            }
            if (start == range.end - 1 && Compare(value, list[start]) >= 0) start++;
            return start;
        }

        // combine a linear search with a binary search to reduce the number of comparisons in situations
        // where have some idea as to how many unique values there are and where the next value might be
        private int FindFirstForward(IList<T> list, T value, Range range, int unique)
        {
            if (range.Length() == 0) return range.start;
            int index, skip = Math.Max(range.Length() / unique, 1);

            for (index = range.start + skip; Compare(list[index - 1], value) < 0; index += skip)
            {
                if (index >= range.end - skip)
                {
                    return BinaryFirst(list, value, new Range(index, range.end));
                }
            }

            return BinaryFirst(list, value, new Range(index - skip, index));
        }

        private int FindLastForward(IList<T> list, T value, Range range, int unique)
        {
            if (range.Length() == 0) return range.start;
            int index, skip = Math.Max(range.Length() / unique, 1);

            for (index = range.start + skip; Compare(value, list[index - 1]) >= 0; index += skip)
            {
                if (index >= range.end - skip)
                {
                    return BinaryLast(list, value, new Range(index, range.end));
                }
            }

            return BinaryLast(list, value, new Range(index - skip, index));
        }

        private int FindFirstBackward(IList<T> list, T value, Range range, int unique)
        {
            if (range.Length() == 0) return range.start;
            int index, skip = Math.Max(range.Length() / unique, 1);

            for (index = range.end - skip; index > range.start && Compare(list[index - 1], value) >= 0; index -= skip)
            {
                if (index < range.start + skip)
                {
                    return BinaryFirst(list, value, new Range(range.start, index));
                }
            }

            return BinaryFirst(list, value, new Range(index, index + skip));
        }

        private int FindLastBackward(IList<T> list, T value, Range range, int unique)
        {
            if (range.Length() == 0) return range.start;
            int index, skip = Math.Max(range.Length() / unique, 1);

            for (index = range.end - skip; index > range.start && Compare(value, list[index - 1]) < 0; index -= skip)
            {
                if (index < range.start + skip)
                {
                    return BinaryLast(list, value, new Range(range.start, index));
                }
            }

            return BinaryLast(list, value, new Range(index, index + skip));
        }

        // n^2 sorting algorithm used to sort tiny chunks of the full list
        private void InsertionSort(IList<T> list, Range range)
        {
            for (int j, i = range.start + 1; i < range.end; i++)
            {
                T temp = list[i];
                for (j = i; j > range.start && Compare(temp, list[j - 1]) < 0; j--)
                    list[j] = list[j - 1];
                list[j] = temp;
            }
        }

        // reverse a range of values within the list
        private static void Reverse(IList<T> list, Range range)
        {
            for (int index = (range.Length() / 2) - 1; index >= 0; index--)
            {
                T swap = list[range.start + index];
                list[range.start + index] = list[range.end - index - 1];
                list[range.end - index - 1] = swap;
            }
        }

        // swap a series of values in the list
        private static void BlockSwap(IList<T> list, int start1, int start2, int block_size)
        {
            for (int index = 0; index < block_size; index++)
            {
                T swap = list[start1 + index];
                list[start1 + index] = list[start2 + index];
                list[start2 + index] = swap;
            }
        }

        // rotate the values in an list ([0 1 2 3] becomes [1 2 3 0] if we rotate by 1)
        // this assumes that 0 <= amount <= range.length()
        private void Rotate(IList<T> list, int amount, Range range, bool use_cache)
        {
            if (range.Length() == 0) return;

            int split;
            if (amount >= 0)
                split = range.start + amount;
            else
                split = range.end + amount;

            var range1 = new Range(range.start, split);
            var range2 = new Range(split, range.end);

            if (use_cache)
            {
                // if the smaller of the two ranges fits into the cache, it's *slightly* faster copying it there and shifting the elements over
                if (range1.Length() <= range2.Length())
                {
                    if (range1.Length() <= _cacheSize)
                    {
                        if (_cache != null)
                        {
                            ListUtility.Copy(list, range1.start, _cache, 0, range1.Length());
                            ListUtility.Copy(list, range2.start, list, range1.start, range2.Length());
                            ListUtility.Copy(_cache, 0, list, range1.start + range2.Length(), range1.Length());
                        }
                        return;
                    }
                }
                else
                {
                    if (range2.Length() <= _cacheSize)
                    {
                        if (_cache != null)
                        {
                            ListUtility.Copy(list, range2.start, _cache, 0, range2.Length());
                            ListUtility.CopyReversed(list, range1.start, list, range2.end - range1.Length(), range1.Length());
                            ListUtility.Copy(_cache, 0, list, range1.start, range2.Length());
                        }
                        return;
                    }
                }
            }

            Reverse(list, range1);
            Reverse(list, range2);
            Reverse(list, range);
        }

        // merge two ranges from one list and save the results into a different list
        private void MergeInto(IList<T> from, Range A, Range B, IList<T> into, int at_index)
        {
            int indexA = A.start;
            int indexB = B.start;
            int insert_index = at_index;
            int lastA = A.end;
            int lastB = B.end;

            while (true)
            {
                if (Compare(from[indexB], from[indexA]) >= 0)
                {
                    into[insert_index] = from[indexA];
                    indexA++;
                    insert_index++;
                    if (indexA == lastA)
                    {
                        // copy the remainder of B into the final list
                        ListUtility.Copy(from, indexB, into, insert_index, lastB - indexB);
                        break;
                    }
                }
                else
                {
                    into[insert_index] = from[indexB];
                    indexB++;
                    insert_index++;
                    if (indexB == lastB)
                    {
                        // copy the remainder of A into the final list
                        ListUtility.Copy(from, indexA, into, insert_index, lastA - indexA);
                        break;
                    }
                }
            }
        }

        // merge operation using an external buffer,
        private void MergeExternal(IList<T> list, Range A, Range B)
        {
            // A fits into the cache, so use that instead of the internal buffer
            int indexA = 0;
            int indexB = B.start;
            int insert_index = A.start;
            int lastA = A.Length();
            int lastB = B.end;

            if (B.Length() > 0 && A.Length() > 0)
            {
                while (true)
                {
                    if (Compare(list[indexB], _cache[indexA]) >= 0)
                    {
                        list[insert_index] = _cache[indexA];
                        indexA++;
                        insert_index++;
                        if (indexA == lastA) break;
                    }
                    else
                    {
                        list[insert_index] = list[indexB];
                        indexB++;
                        insert_index++;
                        if (indexB == lastB) break;
                    }
                }
            }

            // copy the remainder of A into the final list
            if (_cache != null) ListUtility.Copy(_cache, indexA, list, insert_index, lastA - indexA);
        }

        // merge operation using an internal buffer
        private void MergeInternal(IList<T> list, Range A, Range B, Range buffer)
        {
            // whenever we find a value to add to the final list, swap it with the value that's already in that spot
            // when this algorithm is finished, 'buffer' will contain its original contents, but in a different order
            int countA = 0, countB = 0, insert = 0;

            if (B.Length() > 0 && A.Length() > 0)
            {
                while (true)
                {
                    if (Compare(list[B.start + countB], list[buffer.start + countA]) >= 0)
                    {
                        T swap = list[A.start + insert];
                        list[A.start + insert] = list[buffer.start + countA];
                        list[buffer.start + countA] = swap;
                        countA++;
                        insert++;
                        if (countA >= A.Length()) break;
                    }
                    else
                    {
                        T swap = list[A.start + insert];
                        list[A.start + insert] = list[B.start + countB];
                        list[B.start + countB] = swap;
                        countB++;
                        insert++;
                        if (countB >= B.Length()) break;
                    }
                }
            }

            // swap the remainder of A into the final list
            BlockSwap(list, buffer.start + countA, A.start + insert, A.Length() - countA);
        }

        // merge operation without a buffer
        private void MergeInPlace(IList<T> list, Range A, Range B)
        {
            if (A.Length() == 0 || B.Length() == 0) return;

            /*
			 this just repeatedly binary searches into B and rotates A into position.
			 the paper suggests using the 'rotation-based Hwang and Lin algorithm' here,
			 but I decided to stick with this because it had better situational performance

			 (Hwang and Lin is designed for merging subarrays of very different sizes,
			 but WikiSort almost always uses subarrays that are roughly the same size)

			 normally this is incredibly suboptimal, but this function is only called
			 when none of the A or B blocks in any subarray contained 2√A unique values,
			 which places a hard limit on the number of times this will ACTUALLY need
			 to binary search and rotate.

			 according to my analysis the worst case is √A rotations performed on √A items
			 once the constant factors are removed, which ends up being O(n)

			 again, this is NOT a general-purpose solution – it only works well in this case!
			 kind of like how the O(n^2) insertion sort is used in some places
			 */

            A = new Range(A.start, A.end);
            B = new Range(B.start, B.end);

            while (true)
            {
                // find the first place in B where the first item in A needs to be inserted
                int mid = BinaryFirst(list, list[A.start], B);

                // rotate A into place
                int amount = mid - A.end;
                Rotate(list, -amount, new Range(A.start, mid), true);
                if (B.end == mid) break;

                // calculate the new A and B ranges
                B.start = mid;
                A.Set(A.start + amount, B.start);
                A.start = BinaryLast(list, list[A.start], A);
                if (A.Length() == 0) break;
            }
        }

        private void NetSwap(IList<T> list, int[] order, Range range, int x, int y)
        {
            int compare = Compare(list[range.start + x], list[range.start + y]);
            if (compare > 0 || (order[x] > order[y] && compare == 0))
            {
                T swap = list[range.start + x];
                list[range.start + x] = list[range.start + y];
                list[range.start + y] = swap;
                int swap2 = order[x];
                order[x] = order[y];
                order[y] = swap2;
            }
        }

        // bottom-up merge sort combined with an in-place merge algorithm for O(1) memory use
        public override void Sort(IList<T> list, int startingIndex, int length)
        {
            // if the list is of size 0, 1, 2, or 3, just sort them like so:
            if (length < 4)
            {
                _minrunSort.Sort(list, startingIndex, length);
                return;
            }

            // sort groups of 4-8 items at a time using an unstable sorting network,
            // but keep track of the original item orders to force it to be stable
            // http://pages.ripco.net/~jgamble/nw.html
            var iterator = new Iterator(startingIndex, length, 4);
            while (!iterator.Finished())
            {
                int[] order = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
                Range range = iterator.NextRange();

                if (range.Length() == 8)
                {
                    NetSwap(list, order, range, 0, 1); NetSwap(list, order, range, 2, 3);
                    NetSwap(list, order, range, 4, 5); NetSwap(list, order, range, 6, 7);
                    NetSwap(list, order, range, 0, 2); NetSwap(list, order, range, 1, 3);
                    NetSwap(list, order, range, 4, 6); NetSwap(list, order, range, 5, 7);
                    NetSwap(list, order, range, 1, 2); NetSwap(list, order, range, 5, 6);
                    NetSwap(list, order, range, 0, 4); NetSwap(list, order, range, 3, 7);
                    NetSwap(list, order, range, 1, 5); NetSwap(list, order, range, 2, 6);
                    NetSwap(list, order, range, 1, 4); NetSwap(list, order, range, 3, 6);
                    NetSwap(list, order, range, 2, 4); NetSwap(list, order, range, 3, 5);
                    NetSwap(list, order, range, 3, 4);
                }
                else if (range.Length() == 7)
                {
                    NetSwap(list, order, range, 1, 2); NetSwap(list, order, range, 3, 4); NetSwap(list, order, range, 5, 6);
                    NetSwap(list, order, range, 0, 2); NetSwap(list, order, range, 3, 5); NetSwap(list, order, range, 4, 6);
                    NetSwap(list, order, range, 0, 1); NetSwap(list, order, range, 4, 5); NetSwap(list, order, range, 2, 6);
                    NetSwap(list, order, range, 0, 4); NetSwap(list, order, range, 1, 5);
                    NetSwap(list, order, range, 0, 3); NetSwap(list, order, range, 2, 5);
                    NetSwap(list, order, range, 1, 3); NetSwap(list, order, range, 2, 4);
                    NetSwap(list, order, range, 2, 3);

                }
                else if (range.Length() == 6)
                {
                    NetSwap(list, order, range, 1, 2); NetSwap(list, order, range, 4, 5);
                    NetSwap(list, order, range, 0, 2); NetSwap(list, order, range, 3, 5);
                    NetSwap(list, order, range, 0, 1); NetSwap(list, order, range, 3, 4); NetSwap(list, order, range, 2, 5);
                    NetSwap(list, order, range, 0, 3); NetSwap(list, order, range, 1, 4);
                    NetSwap(list, order, range, 2, 4); NetSwap(list, order, range, 1, 3);
                    NetSwap(list, order, range, 2, 3);

                }
                else if (range.Length() == 5)
                {
                    NetSwap(list, order, range, 0, 1); NetSwap(list, order, range, 3, 4);
                    NetSwap(list, order, range, 2, 4);
                    NetSwap(list, order, range, 2, 3); NetSwap(list, order, range, 1, 4);
                    NetSwap(list, order, range, 0, 3);
                    NetSwap(list, order, range, 0, 2); NetSwap(list, order, range, 1, 3);
                    NetSwap(list, order, range, 1, 2);

                }
                else if (range.Length() == 4)
                {
                    NetSwap(list, order, range, 0, 1); NetSwap(list, order, range, 2, 3);
                    NetSwap(list, order, range, 0, 2); NetSwap(list, order, range, 1, 3);
                    NetSwap(list, order, range, 1, 2);
                }
            }
            if (length < 8) return;

            // we need to keep track of a lot of ranges during this sort!
            Range buffer1 = new Range(), buffer2 = new Range();
            Range blockA = new Range(), blockB = new Range();
            Range lastA = new Range(), lastB = new Range();
            var firstA = new Range();
            Range A = new Range(), B = new Range();

            var pull = new Pull[2];
            pull[0] = new Pull();
            pull[1] = new Pull();

            // then merge sort the higher levels, which can be 8-15, 16-31, 32-63, 64-127, etc.
            while (true)
            {
                // if every A and B block will fit into the cache, use a special branch specifically for merging with the cache
                // (we use < rather than <= since the block size might be one more than iterator.length())
                if (iterator.Length() < _cacheSize)
                {

                    // if four subarrays fit into the cache, it's faster to merge both pairs of subarrays into the cache,
                    // then merge the two merged subarrays from the cache back into the original list
                    if ((iterator.Length() + 1) * 4 <= _cacheSize && iterator.Length() * 4 <= length)
                    {
                        iterator.Begin();
                        while (!iterator.Finished())
                        {
                            // merge A1 and B1 into the cache
                            Range A1 = iterator.NextRange();
                            Range B1 = iterator.NextRange();
                            Range A2 = iterator.NextRange();
                            Range B2 = iterator.NextRange();

                            if (Compare(list[B1.end - 1], list[A1.start]) < 0)
                            {
                                // the two ranges are in reverse order, so copy them in reverse order into the cache
                                ListUtility.Copy(list, A1.start, _cache, B1.Length(), A1.Length());
                                ListUtility.Copy(list, B1.start, _cache, 0, B1.Length());
                            }
                            else if (Compare(list[B1.start], list[A1.end - 1]) < 0)
                            {
                                // these two ranges weren't already in order, so merge them into the cache
                                MergeInto(list, A1, B1, _cache, 0);
                            }
                            else
                            {
                                // if A1, B1, A2, and B2 are all in order, skip doing anything else
                                if (Compare(list[B2.start], list[A2.end - 1]) >= 0 && Compare(list[A2.start], list[B1.end - 1]) >= 0) continue;

                                // copy A1 and B1 into the cache in the same order
                                ListUtility.Copy(list, A1.start, _cache, 0, A1.Length());
                                ListUtility.Copy(list, B1.start, _cache, A1.Length(), B1.Length());
                            }
                            A1.Set(A1.start, B1.end);

                            // merge A2 and B2 into the cache
                            if (Compare(list[B2.end - 1], list[A2.start]) < 0)
                            {
                                // the two ranges are in reverse order, so copy them in reverse order into the cache
                                ListUtility.Copy(list, A2.start, _cache, A1.Length() + B2.Length(), A2.Length());
                                ListUtility.Copy(list, B2.start, _cache, A1.Length(), B2.Length());
                            }
                            else if (Compare(list[B2.start], list[A2.end - 1]) < 0)
                            {
                                // these two ranges weren't already in order, so merge them into the cache
                                MergeInto(list, A2, B2, _cache, A1.Length());
                            }
                            else
                            {
                                // copy A2 and B2 into the cache in the same order
                                ListUtility.Copy(list, A2.start, _cache, A1.Length(), A2.Length());
                                ListUtility.Copy(list, B2.start, _cache, A1.Length() + A2.Length(), B2.Length());
                            }
                            A2.Set(A2.start, B2.end);

                            // merge A1 and A2 from the cache into the list
                            var A3 = new Range(0, A1.Length());
                            var B3 = new Range(A1.Length(), A1.Length() + A2.Length());

                            if (Compare(_cache[B3.end - 1], _cache[A3.start]) < 0)
                            {
                                // the two ranges are in reverse order, so copy them in reverse order into the cache
                                ListUtility.Copy(_cache, A3.start, list, A1.start + A2.Length(), A3.Length());
                                ListUtility.Copy(_cache, B3.start, list, A1.start, B3.Length());
                            }
                            else if (Compare(_cache[B3.start], _cache[A3.end - 1]) < 0)
                            {
                                // these two ranges weren't already in order, so merge them back into the list
                                MergeInto(_cache, A3, B3, list, A1.start);
                            }
                            else
                            {
                                // copy A3 and B3 into the list in the same order
                                ListUtility.Copy(_cache, A3.start, list, A1.start, A3.Length());
                                ListUtility.Copy(_cache, B3.start, list, A1.start + A1.Length(), B3.Length());
                            }
                        }

                        // we merged two levels at the same time, so we're done with this level already
                        // (iterator.nextLevel() is called again at the bottom of this outer merge loop)
                        iterator.NextLevel();

                    }
                    else
                    {
                        iterator.Begin();
                        while (!iterator.Finished())
                        {
                            A = iterator.NextRange();
                            B = iterator.NextRange();

                            if (Compare(list[B.end - 1], list[A.start]) < 0)
                            {
                                // the two ranges are in reverse order, so a simple rotation should fix it
                                Rotate(list, A.Length(), new Range(A.start, B.end), true);
                            }
                            else if (Compare(list[B.start], list[A.end - 1]) < 0)
                            {
                                // these two ranges weren't already in order, so we'll need to merge them!
                                ListUtility.Copy(list, A.start, _cache, 0, A.Length());
                                MergeExternal(list, A, B);
                            }
                        }
                    }
                }
                else
                {
                    // this is where the in-place merge logic starts!
                    // 1. pull out two internal buffers each containing √A unique values
                    //     1a. adjust block_size and buffer_size if we couldn't find enough unique values
                    // 2. loop over the A and B subarrays within this level of the merge sort
                    //     3. break A and B into blocks of size 'block_size'
                    //     4. "tag" each of the A blocks with values from the first internal buffer
                    //     5. roll the A blocks through the B blocks and drop/rotate them where they belong
                    //     6. merge each A block with any B values that follow, using the cache or the second internal buffer
                    // 7. sort the second internal buffer if it exists
                    // 8. redistribute the two internal buffers back into the list

                    int block_size = (int)Math.Sqrt(iterator.Length());
                    int buffer_size = iterator.Length() / block_size + 1;

                    // as an optimization, we really only need to pull out the internal buffers once for each level of merges
                    // after that we can reuse the same buffers over and over, then redistribute it when we're finished with this level
                    int index, last, count, pull_index = 0;
                    buffer1.Set(0, 0);
                    buffer2.Set(0, 0);

                    pull[0].Reset();
                    pull[1].Reset();

                    // find two internal buffers of size 'buffer_size' each
                    int find = buffer_size + buffer_size;
                    bool find_separately = false;

                    if (block_size <= _cacheSize)
                    {
                        // if every A block fits into the cache then we won't need the second internal buffer,
                        // so we really only need to find 'buffer_size' unique values
                        find = buffer_size;
                    }
                    else if (find > iterator.Length())
                    {
                        // we can't fit both buffers into the same A or B subarray, so find two buffers separately
                        find = buffer_size;
                        find_separately = true;
                    }

                    // we need to find either a single contiguous space containing 2√A unique values (which will be split up into two buffers of size √A each),
                    // or we need to find one buffer of < 2√A unique values, and a second buffer of √A unique values,
                    // OR if we couldn't find that many unique values, we need the largest possible buffer we can get

                    // in the case where it couldn't find a single buffer of at least √A unique values,
                    // all of the Merge steps must be replaced by a different merge algorithm (MergeInPlace)

                    iterator.Begin();
                    while (!iterator.Finished())
                    {
                        A = iterator.NextRange();
                        B = iterator.NextRange();

                        // check A for the number of unique values we need to fill an internal buffer
                        // these values will be pulled out to the start of A
                        for (last = A.start, count = 1; count < find; last = index, count++)
                        {
                            index = FindLastForward(list, list[last], new Range(last + 1, A.end), find - count);
                            if (index == A.end) break;
                        }
                        index = last;

                        if (count >= buffer_size)
                        {
                            // keep track of the range within the list where we'll need to "pull out" these values to create the internal buffer
                            pull[pull_index].range.Set(A.start, B.end);
                            pull[pull_index].count = count;
                            pull[pull_index].from = index;
                            pull[pull_index].to = A.start;
                            pull_index = 1;

                            if (count == buffer_size + buffer_size)
                            {
                                // we were able to find a single contiguous section containing 2√A unique values,
                                // so this section can be used to contain both of the internal buffers we'll need
                                buffer1.Set(A.start, A.start + buffer_size);
                                buffer2.Set(A.start + buffer_size, A.start + count);
                                break;
                            }
                            else if (find == buffer_size + buffer_size)
                            {
                                // we found a buffer that contains at least √A unique values, but did not contain the full 2√A unique values,
                                // so we still need to find a second separate buffer of at least √A unique values
                                buffer1.Set(A.start, A.start + count);
                                find = buffer_size;
                            }
                            else if (block_size <= _cacheSize)
                            {
                                // we found the first and only internal buffer that we need, so we're done!
                                buffer1.Set(A.start, A.start + count);
                                break;
                            }
                            else if (find_separately)
                            {
                                // found one buffer, but now find the other one
                                buffer1 = new Range(A.start, A.start + count);
                                find_separately = false;
                            }
                            else
                            {
                                // we found a second buffer in an 'A' subarray containing √A unique values, so we're done!
                                buffer2.Set(A.start, A.start + count);
                                break;
                            }
                        }
                        else if (pull_index == 0 && count > buffer1.Length())
                        {
                            // keep track of the largest buffer we were able to find
                            buffer1.Set(A.start, A.start + count);

                            pull[pull_index].range.Set(A.start, B.end);
                            pull[pull_index].count = count;
                            pull[pull_index].from = index;
                            pull[pull_index].to = A.start;
                        }

                        // check B for the number of unique values we need to fill an internal buffer
                        // these values will be pulled out to the end of B
                        for (last = B.end - 1, count = 1; count < find; last = index - 1, count++)
                        {
                            index = FindFirstBackward(list, list[last], new Range(B.start, last), find - count);
                            if (index == B.start) break;
                        }
                        index = last;

                        if (count >= buffer_size)
                        {
                            // keep track of the range within the list where we'll need to "pull out" these values to create the internal buffer
                            pull[pull_index].range.Set(A.start, B.end);
                            pull[pull_index].count = count;
                            pull[pull_index].from = index;
                            pull[pull_index].to = B.end;
                            pull_index = 1;

                            if (count == buffer_size + buffer_size)
                            {
                                // we were able to find a single contiguous section containing 2√A unique values,
                                // so this section can be used to contain both of the internal buffers we'll need
                                buffer1.Set(B.end - count, B.end - buffer_size);
                                buffer2.Set(B.end - buffer_size, B.end);
                                break;
                            }
                            else if (find == buffer_size + buffer_size)
                            {
                                // we found a buffer that contains at least √A unique values, but did not contain the full 2√A unique values,
                                // so we still need to find a second separate buffer of at least √A unique values
                                buffer1.Set(B.end - count, B.end);
                                find = buffer_size;
                            }
                            else if (block_size <= _cacheSize)
                            {
                                // we found the first and only internal buffer that we need, so we're done!
                                buffer1.Set(B.end - count, B.end);
                                break;
                            }
                            else if (find_separately)
                            {
                                // found one buffer, but now find the other one
                                buffer1 = new Range(B.end - count, B.end);
                                find_separately = false;
                            }
                            else
                            {
                                // buffer2 will be pulled out from a 'B' subarray, so if the first buffer was pulled out from the corresponding 'A' subarray,
                                // we need to adjust the end point for that A subarray so it knows to stop redistributing its values before reaching buffer2
                                if (pull[0].range.start == A.start) pull[0].range.end -= pull[1].count;

                                // we found a second buffer in an 'B' subarray containing √A unique values, so we're done!
                                buffer2.Set(B.end - count, B.end);
                                break;
                            }
                        }
                        else if (pull_index == 0 && count > buffer1.Length())
                        {
                            // keep track of the largest buffer we were able to find
                            buffer1.Set(B.end - count, B.end);

                            pull[pull_index].range.Set(A.start, B.end);
                            pull[pull_index].count = count;
                            pull[pull_index].from = index;
                            pull[pull_index].to = B.end;
                        }
                    }

                    // pull out the two ranges so we can use them as internal buffers
                    for (pull_index = 0; pull_index < 2; pull_index++)
                    {
                        int subLength = pull[pull_index].count;

                        if (pull[pull_index].to < pull[pull_index].from)
                        {
                            // we're pulling the values out to the left, which means the start of an A subarray
                            index = pull[pull_index].from;
                            for (count = 1; count < subLength; count++)
                            {
                                index = FindFirstBackward(list, list[index - 1], new Range(pull[pull_index].to, pull[pull_index].from - (count - 1)), subLength - count);
                                var range = new Range(index + 1, pull[pull_index].from + 1);
                                Rotate(list, range.Length() - count, range, true);
                                pull[pull_index].from = index + count;
                            }
                        }
                        else if (pull[pull_index].to > pull[pull_index].from)
                        {
                            // we're pulling values out to the right, which means the end of a B subarray
                            index = pull[pull_index].from + 1;
                            for (count = 1; count < subLength; count++)
                            {
                                index = FindLastForward(list, list[index], new Range(index, pull[pull_index].to), subLength - count);
                                var range = new Range(pull[pull_index].from, index - 1);
                                Rotate(list, count, range, true);
                                pull[pull_index].from = index - 1 - count;
                            }
                        }
                    }

                    // adjust block_size and buffer_size based on the values we were able to pull out
                    buffer_size = buffer1.Length();
                    block_size = iterator.Length() / buffer_size + 1;

                    // the first buffer NEEDS to be large enough to tag each of the evenly sized A blocks,
                    // so this was originally here to test the math for adjusting block_size above
                    //if ((iterator.length() + 1)/block_size > buffer_size) throw new RuntimeException();

                    // now that the two internal buffers have been created, it's time to merge each A+B combination at this level of the merge sort!
                    iterator.Begin();
                    while (!iterator.Finished())
                    {
                        A = iterator.NextRange();
                        B = iterator.NextRange();

                        // remove any parts of A or B that are being used by the internal buffers
                        int start = A.start;
                        if (start == pull[0].range.start)
                        {
                            if (pull[0].from > pull[0].to)
                            {
                                A.start += pull[0].count;

                                // if the internal buffer takes up the entire A or B subarray, then there's nothing to merge
                                // this only happens for very small subarrays, like √4 = 2, 2 * (2 internal buffers) = 4,
                                // which also only happens when cache_size is small or 0 since it'd otherwise use MergeExternal
                                if (A.Length() == 0) continue;
                            }
                            else if (pull[0].from < pull[0].to)
                            {
                                B.end -= pull[0].count;
                                if (B.Length() == 0) continue;
                            }
                        }
                        if (start == pull[1].range.start)
                        {
                            if (pull[1].from > pull[1].to)
                            {
                                A.start += pull[1].count;
                                if (A.Length() == 0) continue;
                            }
                            else if (pull[1].from < pull[1].to)
                            {
                                B.end -= pull[1].count;
                                if (B.Length() == 0) continue;
                            }
                        }

                        if (Compare(list[B.end - 1], list[A.start]) < 0)
                        {
                            // the two ranges are in reverse order, so a simple rotation should fix it
                            Rotate(list, A.Length(), new Range(A.start, B.end), true);
                        }
                        else if (Compare(list[A.end], list[A.end - 1]) < 0)
                        {
                            // these two ranges weren't already in order, so we'll need to merge them!

                            // break the remainder of A into blocks. firstA is the uneven-sized first A block
                            blockA.Set(A.start, A.end);
                            firstA.Set(A.start, A.start + blockA.Length() % block_size);

                            // swap the first value of each A block with the value in buffer1
                            int indexA = buffer1.start;
                            for (index = firstA.end; index < blockA.end; index += block_size)
                            {
                                T swap = list[indexA];
                                list[indexA] = list[index];
                                list[index] = swap;
                                indexA++;
                            }

                            // start rolling the A blocks through the B blocks!
                            // whenever we leave an A block behind, we'll need to merge the previous A block with any B blocks that follow it, so track that information as well
                            lastA.Set(firstA.start, firstA.end);
                            lastB.Set(0, 0);
                            blockB.Set(B.start, B.start + Math.Min(block_size, B.Length()));
                            blockA.start += firstA.Length();
                            indexA = buffer1.start;

                            // if the first unevenly sized A block fits into the cache, copy it there for when we go to Merge it
                            // otherwise, if the second buffer is available, block swap the contents into that
                            if (lastA.Length() <= _cacheSize && _cache != null)
                                ListUtility.Copy(list, lastA.start, _cache, 0, lastA.Length());
                            else if (buffer2.Length() > 0)
                                BlockSwap(list, lastA.start, buffer2.start, lastA.Length());

                            if (blockA.Length() > 0)
                            {
                                while (true)
                                {
                                    // if there's a previous B block and the first value of the minimum A block is <= the last value of the previous B block,
                                    // then drop that minimum A block behind. or if there are no B blocks left then keep dropping the remaining A blocks.
                                    if ((lastB.Length() > 0 && Compare(list[lastB.end - 1], list[indexA]) >= 0) || blockB.Length() == 0)
                                    {
                                        // figure out where to split the previous B block, and rotate it at the split
                                        int B_split = BinaryFirst(list, list[indexA], lastB);
                                        int B_remaining = lastB.end - B_split;

                                        // swap the minimum A block to the beginning of the rolling A blocks
                                        int minA = blockA.start;
                                        for (int findA = minA + block_size; findA < blockA.end; findA += block_size)
                                            if (Compare(list[findA], list[minA]) < 0)
                                                minA = findA;
                                        BlockSwap(list, blockA.start, minA, block_size);

                                        // swap the first item of the previous A block back with its original value, which is stored in buffer1
                                        T swap = list[blockA.start];
                                        list[blockA.start] = list[indexA];
                                        list[indexA] = swap;
                                        indexA++;

                                        // locally merge the previous A block with the B values that follow it
                                        // if lastA fits into the external cache we'll use that (with MergeExternal),
                                        // or if the second internal buffer exists we'll use that (with MergeInternal),
                                        // or failing that we'll use a strictly in-place merge algorithm (MergeInPlace)
                                        if (lastA.Length() <= _cacheSize)
                                            MergeExternal(list, lastA, new Range(lastA.end, B_split));
                                        else if (buffer2.Length() > 0)
                                            MergeInternal(list, lastA, new Range(lastA.end, B_split), buffer2);
                                        else
                                            MergeInPlace(list, lastA, new Range(lastA.end, B_split));

                                        if (buffer2.Length() > 0 || block_size <= _cacheSize)
                                        {
                                            // copy the previous A block into the cache or buffer2, since that's where we need it to be when we go to merge it anyway
                                            if (block_size <= _cacheSize)
                                                ListUtility.Copy(list, blockA.start, _cache, 0, block_size);
                                            else
                                                BlockSwap(list, blockA.start, buffer2.start, block_size);

                                            // this is equivalent to rotating, but faster
                                            // the area normally taken up by the A block is either the contents of buffer2, or data we don't need anymore since we memcopied it
                                            // either way, we don't need to retain the order of those items, so instead of rotating we can just block swap B to where it belongs
                                            BlockSwap(list, B_split, blockA.start + block_size - B_remaining, B_remaining);
                                        }
                                        else
                                        {
                                            // we are unable to use the 'buffer2' trick to speed up the rotation operation since buffer2 doesn't exist, so perform a normal rotation
                                            Rotate(list, blockA.start - B_split, new Range(B_split, blockA.start + block_size), true);
                                        }

                                        // update the range for the remaining A blocks, and the range remaining from the B block after it was split
                                        lastA.Set(blockA.start - B_remaining, blockA.start - B_remaining + block_size);
                                        lastB.Set(lastA.end, lastA.end + B_remaining);

                                        // if there are no more A blocks remaining, this step is finished!
                                        blockA.start += block_size;
                                        if (blockA.Length() == 0)
                                            break;

                                    }
                                    else if (blockB.Length() < block_size)
                                    {
                                        // move the last B block, which is unevenly sized, to before the remaining A blocks, by using a rotation
                                        // the cache is disabled here since it might contain the contents of the previous A block
                                        Rotate(list, -blockB.Length(), new Range(blockA.start, blockB.end), false);

                                        lastB.Set(blockA.start, blockA.start + blockB.Length());
                                        blockA.start += blockB.Length();
                                        blockA.end += blockB.Length();
                                        blockB.end = blockB.start;
                                    }
                                    else
                                    {
                                        // roll the leftmost A block to the end by swapping it with the next B block
                                        BlockSwap(list, blockA.start, blockB.start, block_size);
                                        lastB.Set(blockA.start, blockA.start + block_size);

                                        blockA.start += block_size;
                                        blockA.end += block_size;
                                        blockB.start += block_size;
                                        blockB.end += block_size;

                                        if (blockB.end > B.end)
                                            blockB.end = B.end;
                                    }
                                }
                            }

                            // merge the last A block with the remaining B values
                            if (lastA.Length() <= _cacheSize)
                                MergeExternal(list, lastA, new Range(lastA.end, B.end));
                            else if (buffer2.Length() > 0)
                                MergeInternal(list, lastA, new Range(lastA.end, B.end), buffer2);
                            else
                                MergeInPlace(list, lastA, new Range(lastA.end, B.end));
                        }
                    }

                    // when we're finished with this merge step we should have the one or two internal buffers left over, where the second buffer is all jumbled up
                    // insertion sort the second buffer, then redistribute the buffers back into the list using the opposite process used for creating the buffer

                    // while an unstable sort like quick sort could be applied here, in benchmarks it was consistently slightly slower than a simple insertion sort,
                    // even for tens of millions of items. this may be because insertion sort is quite fast when the data is already somewhat sorted, like it is here
                    InsertionSort(list, buffer2);

                    for (pull_index = 0; pull_index < 2; pull_index++)
                    {
                        int unique = pull[pull_index].count * 2;
                        if (pull[pull_index].from > pull[pull_index].to)
                        {
                            // the values were pulled out to the left, so redistribute them back to the right
                            var buffer = new Range(pull[pull_index].range.start, pull[pull_index].range.start + pull[pull_index].count);
                            while (buffer.Length() > 0)
                            {
                                index = FindFirstForward(list, list[buffer.start], new Range(buffer.end, pull[pull_index].range.end), unique);
                                int amount = index - buffer.end;
                                Rotate(list, buffer.Length(), new Range(buffer.start, index), true);
                                buffer.start += amount + 1;
                                buffer.end += amount;
                                unique -= 2;
                            }
                        }
                        else if (pull[pull_index].from < pull[pull_index].to)
                        {
                            // the values were pulled out to the right, so redistribute them back to the left
                            var buffer = new Range(pull[pull_index].range.end - pull[pull_index].count, pull[pull_index].range.end);
                            while (buffer.Length() > 0)
                            {
                                index = FindLastBackward(list, list[buffer.end - 1], new Range(pull[pull_index].range.start, buffer.start), unique);
                                int amount = buffer.start - index;
                                Rotate(list, amount, new Range(index, buffer.end), true);
                                buffer.start -= amount;
                                buffer.end -= amount + 1;
                                unique -= 2;
                            }
                        }
                    }
                }

                // double the size of each A and B subarray that will be merged in the next level
                if (!iterator.NextLevel()) break;
            }
        }

    }
}
