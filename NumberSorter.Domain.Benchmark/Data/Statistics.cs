using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Data
{
    [ComplexType]
    internal sealed class Statistics
    {
        [Column("statistics_run_count")]
        public int N { get; set; }
        [Column("statistics_min")]
        public double Min { get; set; }
        [Column("statistics_max")]
        public double Max { get; set; }
        [Column("statistics_median")]
        public double Median { get; set; }
        [Column("statistics_mean")]
        public double Mean { get; set; }
        [Column("statistics_lower_fence")]
        public double LowerFence { get; set; }
        [Column("statistics_upper_fence")]
        public double UpperFence { get; set; }
        [Column("statistics_q1")]
        public double Q1 { get; set; }
        [Column("statistics_q3")]
        public double Q3 { get; set; }
        [Column("statistics_interquartile_range")]
        public double InterquartileRange { get; set; }
        [Column("statistics_standard_error")]
        public double StandardError { get; set; }
        [Column("statistics_variance")]
        public double Variance { get; set; }
        [Column("statistics_standard_deviation")]
        public double StandardDeviation { get; set; }
        [Column("statistics_skewness")]
        public double Skewness { get; set; }
        [Column("statistics_kurtosis")]
        public double Kurtosis { get; set; }
        public ConfidenceInterval ConfidenceInterval { get; set; }
        public Percentiles Percentiles { get; set; }
    }
}