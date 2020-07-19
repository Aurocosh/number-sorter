using System;

namespace NumberSorter.Domain.Benchmark.IntegerGenerators
{
    public static class BenchmarkRandomProvider
    {
        private const bool _isSeedStatic = true;
        private const int _seed = 4564;

        public static Random Random => _isSeedStatic ? new Random(_seed) : new Random();
    }
}
