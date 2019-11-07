﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Static
{
    public class SortTest_RandomUnsorted_StaticListGenerator : IEnumerable<object[]>
    {
        private static readonly List<object[]> _data;

        static SortTest_RandomUnsorted_StaticListGenerator()
        {
            _data = new List<object[]>
            {
                new object[] {new List<int>(_list1)},
                new object[] {new List<int>(_list2)},
                new object[] {new List<int>(_list3)},
                new object[] {new List<int>(_list4)},
                new object[] {new List<int>(_list5)},

                new object[] {new List<int>(_list6)},
                new object[] {new List<int>(_list7)},
                new object[] {new List<int>(_list8)},
                new object[] {new List<int>(_list9)},
                new object[] {new List<int>(_list10)},

                new object[] {new List<int>(_list11)},
            };
        }

        public IEnumerable<object[]> GetEnumerable() => _data;
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private static readonly List<int> _list1 = new List<int> { -5, -2, -1, 1, 3, -4, -3, 2 };
        private static readonly List<int> _list2 = new List<int> { 2, -1, -2, -4, -3, -5, 3, 1 };
        private static readonly List<int> _list3 = new List<int> { 1, 5, 4, 3, 6, 7, 2 };
        private static readonly List<int> _list4 = new List<int> { 89, 13, 54, -62, 70, -9, 97, -15, 57, -60, 53, -86, 92, -65, 9, -25, -48, 13, 79, -73, -39, 40, 11, 42, 45, -86, 72, -57, 91, 96, 77, 29, -72, -85, -12, 66, -66, -28, -69, -56, -78, 48, -2, 60, 95, 55, -18, 3, 44, 2, -66, -57, 42, -41, 99, -52, -98, -89, -8, 56, 19, 53, -7, 61, -37, -76, 86, 76, 47, 42, -60, 79, -18, 34, -95, 12, -59, 51, 46, 90, 32, -31, -2, -11, 61, 34, 86, 68, -84, -61, 64, -78, 80, -25, 25, -32, -45, 36, -4, 71 };
        private static readonly List<int> _list5 = new List<int> { -49, -75, 57, 61, 75, -98, 89, -94, 60, 23, 56, 13, -45, -85, -30, 32, -98, -5, -38, -60, 23, -32, -34, -72, -25, 85, -35, 28, -29, 80, -44, 11, -39, -7, 5, -49, -37, -51, -16, 10, -73, 44, 40, 81, 43, 65, 90, -83, 99, 99, 8, -54, 89, 46, -17, 83, 58, 28, -15, 90, 36, -40, 35, 80, -33, -55, -49, 61, 9, -82, -32, 52, -90, 52, -87, 79, 28, -15, -15, 10, 94, -52, 28, -28, -29, -91, 21, 15, -90, 22, -7, 34, 63, -6, 73, 66, -91, 59, 14, -2 };

        private static readonly List<int> _list6 = new List<int> { 19, 9, 90, 30, 0, 89, -47, 55, 65, -18, -86, -62, -25, 96, 52, -85, 96, 7, 4, 9, -64, 36, -32, -24, 78, -61, -62, 6, 42, -55, 61, 56, 77, 33, 32, 41, -81, 85, -1, 5, 69, -68, 47, 36, 0, -24, 31, 95, -57, 12, 29, 26, 62, -73, -84, 82, -60, 14, 52, -39, 50, 46, -87, 20, 20, -42, -40, 41, -37, -90, 95, -89, -93, 99, 39, -96, 89, 31, 75, 2, 8, -58, -37, -70, 15, -66, 93, 49, 17, 49, 0, 4, -67, -62, 54, -78, -81, -74, -85, -58 };
        private static readonly List<int> _list7 = new List<int> { 78, 18, 46, -77, 7, -69, -88, -76, 77, 46, 37, 0, -90, 1, 1, -8, -10, 46, -74, 96, 26, 40, 95, 84, 44, 18, 30, -68, -98, -90, -97, -17, -53, -88, 36, 3, -29, -24, 20, -58, 11, -73, 98, 21, 24, -99, -86, 38, -59, 24, -92, 3, 14, -82, -66, -62, 22, 62, -22, 88, 0, 80, -79, 67, 43, -47, -48, 98, 65, 97, -80, -86, -75, 84, -16, -1, 41, -26, -41, 42, 4, 91, 91, -23, -99, 0, 68, -71, 77, -3, 80, 9, 97, 32, 41, 31, -95, -69, 78, -30 };
        private static readonly List<int> _list8 = new List<int> { -66, -61, -96, 79, -26, 35, -7, 24, -27, -83, 78, -82, -72, -35, 60, -66, 2, -55, -98, 63, -33, 44, -72, -98, -98, -46, -5, 73, -94, -53, -19, -62, -26, 38, 15, -41, -31, -89, 54, 61, -22, 26, 7, 80, -64, 13, -71, -38, -46, -34, 64, 51, 44, 29, 61, -11, -89, -98, 76, -80, -60, 20, 18, -75, 35, 40, 44, -10, 49, 1, 65, -10, -10, -60, -16, 40, -63, -53, 66, -12, -76, -68, 18, 39, -17, 30, -6, 44, 76, -74, -53, -33, -65, 34, 21, 58, -93, -19, -56, -5 };
        private static readonly List<int> _list9 = new List<int> { 12, -23, -97, 48, 30, -31, 97, -44, -5, -80, -30, -87, 70, -91, 80, 17, -83, -100, -14, -91, 5, 62, -63, 87, 3, -18, -76, 63, -98, -75, 0, 45, -40, 34, -41, -69, -60, 59, -92, 55, -23, -52, 72, 66, -41, 81, -59, -19, 4, -8, 3, -28, -64, -30, -47, 49, -60, -85, -55, -53, -55, -66, -46, -31, 19, 24, 53, -65, 50, 48, -23, -43, 91, 30, 31, -43, 89, -30, 28, 21, -60, 42, -42, 9, 22, -73, 8, 90, -19, 9, 91, -76, 14, 60, 10, -57, 94, 2, -54, 34 };
        private static readonly List<int> _list10 = new List<int> { -62, 1, 47, 50, -74, -5, -14, -23, 25, 25, 64, 82, -39, 82, -77, 53, 14, -70, 56, 45, -51, -33, 19, -47, 91, 19, -25, 19, -80, -90, 76, -72, 27, 18, -34, 33, -1, 19, -45, 80, 28, -14, 14, -57, 84, 5, -28, -38, 37, 12, -47, -86, 68, -85, -97, 70, 81, -7, 58, 7, -80, 66, -43, 14, 49, 35, -46, 42, 15, -11, -46, 94, 75, 76, -84, 63, 53, -24, -31, -14, -54, -87, 82, 8, 56, 62, -41, 12, 15, -5, 52, -95, 61, 48, 60, 62, -72, 99, -6, -52 };

        private static readonly List<int> _list11 = new List<int> { -99, -98, -98, -92, -92, -91, -89, -88, -85, -82, -74, -72, -66, -62, -61, -60, -60, -60, -60, -60, -53, -51, -51, -50, -47, -44, -44, -36, -35, -35, -33, -31, -31, -31, -31, -28, -25, -25, -25, -41, -25, -23, -14, -12, -10, -23, -16, -8, -5, -5, -4, -3, -3, -3, -3, 3, -66, 7, 13, 20, 21, 22, 25, 26, 29, 29, 31, 32, 33, 35, 36, 40, 43, 44, 45, 46, 46, 48, 57, 59, 69, 70, 74, 76, 77, 78, 79, 79, 7, 80, 80, 80, 84, 85, 85, 86, 90, 95, 97, 98 };
    }
}