﻿using AutoFixture.Xunit2;
using NumberSorter.Domain.Algorhythm;
using NumberSorter.Domain.Logic.Comparer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace NumberSorter.Domain.Tests.SortTests
{
    public abstract class SortTests
    {
        private IComparer<int> _comparer;
        private readonly ISortAlgorhythm<int> _sort;


        protected SortTests()
        {
            _comparer = new IntComparer();
            _sort = GetAlgorhythm(_comparer);
        }

        protected abstract ISortAlgorhythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Theory]
        [ClassData(typeof(StaticRandom100IntegerListGenerator))]
        public void TestSort100RandomStatic(List<int> testData)
        {
            TestSort(testData);
        }

        [Theory]
        [ClassData(typeof(DynamicRandom100IntegerListGenerator))]
        public void TestSort100RandomDynamic(List<int> testData)
        {
            TestSort(testData);
        }

        [Theory, AutoData]
        public void TestAutoData([MinLength(10), MaxLength(50)]int[] testData)
        {
            TestSort(testData);
        }

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
