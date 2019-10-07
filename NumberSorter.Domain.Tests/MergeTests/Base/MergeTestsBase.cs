using AutoFixture.Xunit2;
using NumberSorter.Core.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm;
using NumberSorter.Core.Logic.Algorhythm.Merge.Base;
using NumberSorter.Core.Logic.Comparer;
using NumberSorter.Domain.Tests.SortTests.Base.IntegerGenerators.Dynamic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace NumberSorter.Domain.Tests.MergeTests.Base
{
    public abstract class MergeTestsBase
    {
        private IComparer<int> _comparer;
        private readonly ILocalMergeAlgothythm<int> _merge;


        protected MergeTestsBase()
        {
            _comparer = new IntComparer();
            _merge = GetAlgorhythm(_comparer);
        }

        protected abstract ILocalMergeAlgothythm<int> GetAlgorhythm(IComparer<int> comparer);

        [Theory]
        [ClassData(typeof(MergeTest_TwoFullySortedParts_FirstBiggerThenSecond_DynamicListGenerator))]
        public void MergingTwoSorted_JustNeedToSwapParts_RandomArray(List<int> input, SortRun firstRun, SortRun secondRun)
        {
            var result = new List<int>(input);
            _merge.Merge(result, firstRun, secondRun);
            bool fullySorted = ListUtility.IsSorted(result, _comparer);
            var message = GetResultMessage(fullySorted, input, result, firstRun, secondRun);
            Assert.True(fullySorted, message);
        }

        private static string GetResultMessage(bool isFullySorted, IList<int> input, IList<int> result, SortRun firstRun, SortRun secondRun)
        {
            if (isFullySorted)
                return "";
            var inputString = string.Join("\t", input);
            var resultString = string.Join("\t", result);
            return $"Failed to sort list:\nFirst run: {firstRun}\nSecond run: {secondRun}\nInput: {inputString}\nResult: {resultString}";
        }
    }
}
