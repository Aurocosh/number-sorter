using System;

namespace NumberSorter.Domain.Tests
{
    public static class TestsRandomProvider
    {
        private const bool _isSeedStatic = false;
        private const int _seed = 4564;

        public static Random Random => _isSeedStatic ? new Random(_seed) : new Random();
    }
}
