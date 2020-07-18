using BenchmarkDotNet.Running;
using NumberSorter.Domain.Benchmark.Benchmarks.Upload;
using System;

namespace NumberSorter.Domain.Benchmark
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
            //BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, new DebugInProcessConfig());

            var databaseUploader = new DatabaseUploader();
            databaseUploader.Upload();
        }
    }
}
