using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic;
using NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Static;
using System.Collections.Generic;
using Xunit;

namespace NumberSorter.Domain.Tests.SortTests.Base
{
    public abstract class SortTestsBase
    {
        private IComparer<int> _comparer;
        private IIntegerSortAlgorhythm _integerSort;
        private readonly ISortAlgorhythm<int> _sort;

        protected SortTestsBase()
        {
            _comparer = new IntComparer();
            _integerSort = GetIntAlgorhythm();
            _sort = GetAlgorhythm(_comparer);
        }

        protected virtual IIntegerSortAlgorhythm GetIntAlgorhythm() => null;
        protected virtual ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer) => null;

        [Theory]
        [ClassData(typeof(SortTest_RandomUnsorted_StaticListGenerator))]
        public void ListSort_RandomUnsorted_StaticList(List<int> testData)
        {
            TestSort(testData);
        }


        [Theory]
        [ClassData(typeof(SortTest_RandomUnsorted_DynamicListGenerator))]
        public void ListSort_RandomUnsorted_DynamicList(List<int> testData)
        {
            TestSort(testData);
        }

        [Theory]
        [ClassData(typeof(SortTest_PartiallySorted_StaticListGenerator))]
        public void ListSort_PartiallySorted_StaticList(List<int> testData)
        {
            TestSort(testData);
        }

        [Theory]
        [ClassData(typeof(SortTest_PartiallySorted_DynamicListGenerator))]
        public void ListSort_PartiallySorted_DynamicList(List<int> testData)
        {
            TestSort(testData);
        }

        [Theory]
        [ClassData(typeof(SortTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator))]
        public void ListSort_TwoFullySortedParts_FirstBiggerThenSecond_DynamicList(List<int> testData)
        {
            TestSort(testData);
        }

        //[Theory, AutoData]
        //public void ListSort_RandomUnsorted_AutoData([MinLength(10), MaxLength(50)]int[] testData)
        //{
        //    TestSort(testData);
        //}

        private void TestSort(IList<int> input)
        {
            var result = new List<int>(input);

            if (_integerSort != null)
                _integerSort.Sort(result);
            else
                _sort.Sort(result);

            bool fullySorted = ListUtility.IsSorted(result, _comparer);
            bool validSortedValues = ListUtility.IsSortedValuesValid(input, result, _comparer);
            bool sorted = fullySorted && validSortedValues;

            var message = GetResultMessage(sorted && validSortedValues, input, result);
            Assert.True(sorted, message);
        }

        private static string GetResultMessage(bool isFullySorted, IList<int> input, IList<int> result)
        {
            if (isFullySorted)
                return "";
            var inputString = string.Join("\t", input);
            var resultString = string.Join("\t", result);
            return $"Failed to sort list:\n Input: {inputString}\n Result: {resultString}";
        }
    }
}
