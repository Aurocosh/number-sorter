using AutoFixture.Xunit2;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic;
using NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace NumberSorter.Domain.Tests.SortTests.Base
{
    public abstract class SortTestsBase
    {
        private IComparer<int> _comparer;
        private readonly ISortAlgorhythm<int> _sort;


        protected SortTestsBase()
        {
            _comparer = new IntComparer();
            _sort = GetAlgorhythm(_comparer);
        }

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

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
            _sort.Sort(result);
            bool fullySorted = ListUtility.IsSorted(result, _comparer);
            var message = GetResultMessage(fullySorted, input, result);
            Assert.True(fullySorted, message);
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
