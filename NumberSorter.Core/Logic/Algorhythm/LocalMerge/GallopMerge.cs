using NumberSorter.Core.Logic.Algorhythm.LocalMerge.Base;
using NumberSorter.Core.Logic.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NumberSorter.Core.Logic.Algorhythm.LocalMerge
{
    #region Licence
    /*
     * Copyright (C) 2008 The Android Open Source Project
     *
     * Licensed under the Apache License, Version 2.0 (the "License");
     * you may not use this file except in compliance with the License.
     * You may obtain a copy of the License at
     *
     *      http://www.apache.org/licenses/LICENSE-2.0
     *
     * Unless required by applicable law or agreed to in writing, software
     * distributed under the License is distributed on an "AS IS" BASIS,
     * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
     * See the License for the specific language governing permissions and
     * limitations under the License.
     */
    #endregion

    #region Notes
    //------------------------------------------------------------------------------
    // Java implementation:
    //
    // A stable, adaptive, iterative mergesort that requires far fewer than
    // n lg(n) comparisons when running on partially sorted arrays, while
    // offering performance comparable to a traditional mergesort when run
    // on random arrays.  Like all proper mergesorts, this sort is stable and
    // runs O(n log n) time (worst case).  In the worst case, this sort requires
    // temporary storage space for n/2 object references; in the best case,
    // it requires only a small constant amount of space.
    // 
    // This implementation was adapted from Tim Peters's list sort for
    // Python, which is described in detail here:
    // http://svn.python.org/projects/python/trunk/Objects/listsort.txt
    // 
    // Tim's C code may be found here:
    // http://svn.python.org/projects/python/trunk/Objects/listobject.c
    // 
    // The underlying techniques are described in this paper (and may have
    // even earlier origins):
    // 
    // "Optimistic Sorting and Information Theoretic Complexity"
    // Peter McIlroy
    // SODA (Fourth Annual ACM-SIAM Symposium on Discrete Algorithms),
    // pp 467-474, Austin, Texas, 25-27 January 1993.
    // 
    // While the API to this class consists solely of static methods, it is
    // (privately) instantiable; a TimSort instance holds the state of an ongoing
    // sort, assuming the input array is large enough to warrant the full-blown
    // TimSort. Small arrays are sorted in place, using a binary insertion sort.
    // 
    // author: Josh Bloch
    //------------------------------------------------------------------------------
    // C# implementation:
    //
    // This implementation was adapted from Josh Bloch array sort for Java, 
    // which has been found here:
    // http://gee.cs.oswego.edu/cgi-bin/viewcvs.cgi/jsr166/src/main/java/util/TimSort.java?view=co
    // 
    // author: Milosz Krajewski
    //------------------------------------------------------------------------------
    #endregion

    public sealed class GallopMerge<T> : GenericMergeAlgorhythm<T>
    {
        private const int _startingMinGallop = 7;

        private int _minGallop;
        private T[] _mergeBuffer;

        public GallopMerge(IComparer<T> comparer) : base(comparer)
        {
            _minGallop = _startingMinGallop;
            _mergeBuffer = Array.Empty<T>();
        }

        public override void Merge(IList<T> list, SortRun firstRun, SortRun secondRun)
        {
            if (firstRun.Length == 0 || secondRun.Length == 0)
                return;
            if (Compare(list, firstRun.LastIndex, secondRun.FirstIndex) <= 0)
                return;

            // Find where the first element of run2 goes in run1. Prior elements
            // in run1 can be ignored (because they're already in place).
            var k = GallopRight(list[secondRun.FirstIndex], list, firstRun.FirstIndex, firstRun.Length, 0);
            Debug.Assert(k >= 0);

            var newFirstRun = new SortRun(firstRun.FirstIndex + k, firstRun.Length - k);
            if (firstRun.Length == 0)
            {
                return;
            }

            // Find where the last element of run1 goes in run2. Subsequent elements
            // in run2 can be ignored (because they're already in place).
            var len2 = GallopLeft(list[newFirstRun.LastIndex], list, secondRun.FirstIndex, secondRun.Length, secondRun.Length - 1);
            Debug.Assert(len2 >= 0);
            if (len2 == 0)
            {
                return;
            }

            var newSecondRun = new SortRun(secondRun.FirstIndex, len2);

            // Merge remaining runs, using tmp array with min(len1, len2) elements
            if (newFirstRun.Length <= len2)
            {
                MergeLo(list, newFirstRun.FirstIndex, newFirstRun.Length, newSecondRun.FirstIndex, newSecondRun.Length);
            }
            else
            {
                MergeHi(list, newFirstRun.FirstIndex, newFirstRun.Length, newSecondRun.FirstIndex, newSecondRun.Length);
            }
        }

        internal int GallopLeft(T key, IList<T> array, int lo, int length, int hint)
        {
            var a = array;
            {
                // fixed (...)
                Debug.Assert(length > 0 && hint >= 0 && hint < length);
                var lastOfs = 0;
                var ofs = 1;

                if (Compare(key, array[lo + hint]) > 0)
                {
                    // Gallop right until a[base+hint+lastOfs] < key <= a[base+hint+ofs]
                    var maxOfs = length - hint;
                    while (ofs < maxOfs && Compare(key, array[lo + hint + ofs]) > 0) // comparer(key, a[lo + hint + ofs]) > 0
                    {
                        lastOfs = ofs;
                        ofs = (ofs << 1) + 1;
                        if (ofs <= 0) // int overflow
                        {
                            ofs = maxOfs;
                        }
                    }
                    if (ofs > maxOfs)
                    {
                        ofs = maxOfs;
                    }

                    // Make offsets relative to base
                    lastOfs += hint;
                    ofs += hint;
                }
                else // if (key <= a[base + hint])
                {
                    // Gallop left until a[base+hint-ofs] < key <= a[base+hint-lastOfs]
                    var maxOfs = hint + 1;
                    while (ofs < maxOfs && Compare(key, a[lo + hint - ofs]) <= 0) // comparer(key, a[lo + hint - ofs]) <= 0
                    {
                        lastOfs = ofs;
                        ofs = (ofs << 1) + 1;
                        if (ofs <= 0) // int overflow
                        {
                            ofs = maxOfs;
                        }
                    }
                    if (ofs > maxOfs)
                    {
                        ofs = maxOfs;
                    }

                    // Make offsets relative to base
                    var tmp = lastOfs;
                    lastOfs = hint - ofs;
                    ofs = hint - tmp;
                }
                Debug.Assert(-1 <= lastOfs && lastOfs < ofs && ofs <= length);

                // Now a[base+lastOfs] < key <= a[base+ofs], so key belongs somewhere
                // to the right of lastOfs but no farther right than ofs.  Do a binary
                // search, with invariant a[base + lastOfs - 1] < key <= a[base + ofs].
                lastOfs++;
                while (lastOfs < ofs)
                {
                    var m = lastOfs + ((ofs - lastOfs) >> 1);

                    if (Compare(key, a[lo + m]) > 0) // comparer(key, a[lo + m]) > 0
                    {
                        lastOfs = m + 1; // a[base + m] < key
                    }
                    else
                    {
                        ofs = m; // key <= a[base + m]
                    }
                }
                Debug.Assert(lastOfs == ofs); // so a[base + ofs - 1] < key <= a[base + ofs]
                return ofs;
            } // fixed (...)
        }

        internal int GallopRight(T key, IList<T> array, int lo, int length, int hint)
        {
            var a = array;
            {
                Debug.Assert(length > 0 && hint >= 0 && hint < length);

                var ofs = 1;
                var lastOfs = 0;
                if (Compare(key, array[lo + hint]) < 0) // comparer(key, a[lo + hint]) < 0
                {
                    // Gallop left until a[b+hint - ofs] <= key < a[b+hint - lastOfs]
                    var maxOfs = hint + 1;
                    while (ofs < maxOfs && Compare(key, a[lo + hint - ofs]) < 0)
                    {
                        lastOfs = ofs;
                        ofs = (ofs << 1) + 1;
                        if (ofs <= 0) // int overflow
                        {
                            ofs = maxOfs;
                        }

                    }
                    if (ofs > maxOfs)
                    {
                        ofs = maxOfs;
                    }

                    // Make offsets relative to b
                    var tmp = lastOfs;
                    lastOfs = hint - ofs;
                    ofs = hint - tmp;
                }
                else
                {
                    // a[b + hint] <= key
                    // Gallop right until a[b+hint + lastOfs] <= key < a[b+hint + ofs]
                    var maxOfs = length - hint;
                    while (ofs < maxOfs && Compare(key, a[lo + hint + ofs]) >= 0)
                    {
                        lastOfs = ofs;
                        ofs = (ofs << 1) + 1;
                        if (ofs <= 0) // int overflow
                        {
                            ofs = maxOfs;
                        }
                    }
                    if (ofs > maxOfs)
                    {
                        ofs = maxOfs;
                    }

                    // Make offsets relative to b
                    lastOfs += hint;
                    ofs += hint;
                }
                Debug.Assert(-1 <= lastOfs && lastOfs < ofs && ofs <= length);

                // Now a[b + lastOfs] <= key < a[b + ofs], so key belongs somewhere to
                // the right of lastOfs but no farther right than ofs.  Do a binary
                // search, with invariant a[b + lastOfs - 1] <= key < a[b + ofs].
                lastOfs++;
                while (lastOfs < ofs)
                {
                    var m = lastOfs + ((ofs - lastOfs) >> 1);

                    if (Compare(key, a[lo + m]) < 0)
                    {
                        ofs = m; // key < a[b + m]
                    }
                    else
                    {
                        lastOfs = m + 1; // a[b + m] <= key
                    }
                }

                Debug.Assert(lastOfs == ofs); // so a[b + ofs - 1] <= key < a[b + ofs]
                return ofs;
            }
        }

        private void MergeLo(IList<T> list, int base1, int len1, int base2, int len2)
        {
            Debug.Assert(len1 > 0 && len2 > 0 && base1 + len1 == base2);

            // Copy first run into temp array
            var array = list;
            ResiseBufferIfNeeded(len1, list.Count);

            var m = _mergeBuffer;
            var a = array;
            {
                // fixed (...)


                ListUtility.Copy(a, base1, m, 0, len1);

                var cursor1 = 0; // Indexes into tmp array
                var cursor2 = base2; // Indexes int a
                var dest = base1; // Indexes int a

                // Move first element of second run and deal with degenerate cases
                a[dest++] = a[cursor2++];
                if (--len2 == 0)
                {
                    ListUtility.Copy(m, cursor1, a, dest, len1);
                    return;
                }
                if (len1 == 1)
                {
                    ListUtility.Copy(a, cursor2, a, dest, len2);
                    a[dest + len2] = m[cursor1]; // Last elt of run 1 to end of merge
                    return;
                }

                var minGallop = _minGallop;

                while (true)
                {
                    var count1 = 0; // Number of times in a row that first run won
                    var count2 = 0; // Number of times in a row that second run won

                    // Do the straightforward thing until (if ever) one run starts
                    // winning consistently.
                    do
                    {
                        Debug.Assert(len1 > 1 && len2 > 0);
                        if (Compare(a[cursor2], m[cursor1]) < 0) // c(a[cursor2], m[cursor1]) < 0
                        {
                            a[dest++] = a[cursor2++];
                            count2++;
                            count1 = 0;
                            if (--len2 == 0)
                            {
                                goto break_outer;
                            }
                        }
                        else
                        {
                            a[dest++] = m[cursor1++];
                            count1++;
                            count2 = 0;
                            if (--len1 == 1)
                            {
                                goto break_outer;
                            }
                        }
                    } while ((count1 | count2) < minGallop);

                    // One run is winning so consistently that galloping may be a
                    // huge win. So try that, and continue galloping until (if ever)
                    // neither run appears to be winning consistently anymore.
                    do
                    {
                        Debug.Assert(len1 > 1 && len2 > 0);
                        count1 = GallopRight(a[cursor2], _mergeBuffer, cursor1, len1, 0);
                        if (count1 != 0)
                        {
                            ListUtility.Copy(m, cursor1, a, dest, count1);
                            dest += count1;
                            cursor1 += count1;
                            len1 -= count1;
                            if (len1 <= 1) // len1 == 1 || len1 == 0
                            {
                                goto break_outer;
                            }
                        }
                        a[dest++] = a[cursor2++];
                        if (--len2 == 0)
                        {
                            goto break_outer;
                        }

                        count2 = GallopLeft(m[cursor1], array, cursor2, len2, 0);
                        if (count2 != 0)
                        {
                            ListUtility.Copy(a, cursor2, a, dest, count2);
                            dest += count2;
                            cursor2 += count2;
                            len2 -= count2;
                            if (len2 == 0)
                            {
                                goto break_outer;
                            }
                        }
                        a[dest++] = m[cursor1++];
                        if (--len1 == 1)
                        {
                            goto break_outer;
                        }
                        minGallop--;
                    } while (count1 >= _startingMinGallop | count2 >= _startingMinGallop);

                    if (minGallop < 0)
                    {
                        minGallop = 0;
                    }
                    minGallop += 2; // Penalize for leaving gallop mode
                } // End of "outer" loop

            break_outer: // goto me! ;)

                _minGallop = minGallop < 1 ? 1 : minGallop; // Write back to field

                if (len1 == 1)
                {
                    Debug.Assert(len2 > 0);
                    ListUtility.Copy(a, cursor2, a, dest, len2);
                    a[dest + len2] = m[cursor1]; //  Last elt of run 1 to end of merge
                }
                else if (len1 == 0)
                {
                    throw new ArgumentException("Comparison method violates its general contract!");
                }
                else
                {
                    Debug.Assert(len2 == 0);
                    Debug.Assert(len1 > 1);
                    ListUtility.Copy(m, cursor1, a, dest, len1);
                }
            }
        }

        private void MergeHi(IList<T> list, int base1, int len1, int base2, int len2)
        {
            Debug.Assert(len1 > 0 && len2 > 0 && base1 + len1 == base2);

            // Copy second run into temp array
            var array = list; // For performance
            ResiseBufferIfNeeded(len2, list.Count);

            var m = _mergeBuffer;
            var a = array;
            {
                // fixed (...)
                ListUtility.Copy(a, base2, m, 0, len2);

                var cursor1 = base1 + len1 - 1; // Indexes into a
                var cursor2 = len2 - 1; // Indexes into mergeBuffer array
                var dest = base2 + len2 - 1; // Indexes into a

                // Move last element of first run and deal with degenerate cases
                a[dest--] = a[cursor1--];
                if (--len1 == 0)
                {
                    ListUtility.Copy(m, 0, a, dest - (len2 - 1), len2);
                    return;
                }
                if (len2 == 1)
                {
                    dest -= len1;
                    cursor1 -= len1;
                    ListUtility.CopyReversed(a, cursor1 + 1, a, dest + 1, len1);
                    a[dest] = m[cursor2];
                    return;
                }

                var minGallop = _minGallop;

                while (true)
                {
                    var count1 = 0; // Number of times in a row that first run won
                    var count2 = 0; // Number of times in a row that second run won

                    // Do the straightforward thing until (if ever) one run appears to win consistently.
                    do
                    {
                        if (Compare(m[cursor2], a[cursor1]) < 0) // c(m[cursor2], a[cursor1]) < 0
                        {
                            a[dest--] = a[cursor1--];
                            count1++;
                            count2 = 0;
                            if (--len1 == 0)
                            {
                                goto break_outer;
                            }
                        }
                        else
                        {
                            a[dest--] = m[cursor2--];
                            count2++;
                            count1 = 0;
                            if (--len2 == 1)
                            {
                                goto break_outer;
                            }
                        }
                    } while ((count1 | count2) < minGallop);

                    // One run is winning so consistently that galloping may be a
                    // huge win. So try that, and continue galloping until (if ever)
                    // neither run appears to be winning consistently anymore.
                    do
                    {
                        Debug.Assert(len1 > 0 && len2 > 1);
                        count1 = len1 - GallopRight(m[cursor2], array, base1, len1, len1 - 1);
                        if (count1 != 0)
                        {
                            dest -= count1;
                            cursor1 -= count1;
                            len1 -= count1;
                            ListUtility.CopyReversed(a, cursor1 + 1, a, dest + 1, count1);
                            if (len1 == 0)
                            {
                                goto break_outer;
                            }
                        }
                        a[dest--] = m[cursor2--];
                        if (--len2 == 1)
                        {
                            goto break_outer;
                        }

                        count2 = len2 - GallopLeft(a[cursor1], _mergeBuffer, 0, len2, len2 - 1);
                        if (count2 != 0)
                        {
                            dest -= count2;
                            cursor2 -= count2;
                            len2 -= count2;
                            ListUtility.Copy(m, cursor2 + 1, a, dest + 1, count2);
                            if (len2 <= 1) // len2 == 1 || len2 == 0
                            {
                                goto break_outer;
                            }
                        }
                        a[dest--] = a[cursor1--];
                        if (--len1 == 0)
                        {
                            goto break_outer;
                        }
                        minGallop--;
                    } while (count1 >= _startingMinGallop | count2 >= _startingMinGallop);

                    if (minGallop < 0)
                    {
                        minGallop = 0;
                    }
                    minGallop += 2; // Penalize for leaving gallop mode
                } // End of "outer" loop

            break_outer: // goto me! ;)

                _minGallop = minGallop < 1 ? 1 : minGallop; // Write back to field

                if (len2 == 1)
                {
                    Debug.Assert(len1 > 0);
                    dest -= len1;
                    cursor1 -= len1;
                    ListUtility.CopyReversed(a, cursor1 + 1, a, dest + 1, len1);
                    a[dest] = m[cursor2]; // Move first elt of run2 to front of merge
                }
                else if (len2 == 0)
                {
                    throw new ArgumentException("Comparison method violates its general contract!");
                }
                else
                {
                    Debug.Assert(len1 == 0);
                    Debug.Assert(len2 > 0);
                    ListUtility.Copy(m, 0, a, dest - (len2 - 1), len2);
                }
            } // fixed (...)
        }

        private void ResiseBufferIfNeeded(int minCapacity, int listLength)
        {
            if (_mergeBuffer.Length < minCapacity)
            {
                // Compute smallest power of 2 > minCapacity
                var newSize = minCapacity;
                newSize |= newSize >> 1;
                newSize |= newSize >> 2;
                newSize |= newSize >> 4;
                newSize |= newSize >> 8;
                newSize |= newSize >> 16;
                newSize++;

                newSize = newSize < 0 ? minCapacity : Math.Min(newSize, listLength >> 1);

                _mergeBuffer = new T[newSize];
            }
        }
    }
}
