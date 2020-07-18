using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Data
{
    [Table("host_environment_info")]
    internal sealed class HostEnvironmentInfo
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("benchmark_dot_net_caption")]
        public string BenchmarkDotNetCaption { get; set; }
        [Column("benchmark_dot_net_version")]
        public string BenchmarkDotNetVersion { get; set; }
        [Column("os_version")]
        public string OsVersion { get; set; }
        [Column("processor_name")]
        public string ProcessorName { get; set; }
        [Column("physical_processor_count")]
        public int PhysicalProcessorCount { get; set; }
        [Column("physical_core_count")]
        public int PhysicalCoreCount { get; set; }
        [Column("logical_core_count")]
        public int LogicalCoreCount { get; set; }
        [Column("runtime_version")]
        public string RuntimeVersion { get; set; }
        [Column("architecture")]
        public string Architecture { get; set; }
        [Column("has_attached_debugger")]
        public bool HasAttachedDebugger { get; set; }
        [Column("has_ryu_jit")]
        public bool HasRyuJit { get; set; }
        [Column("configuration")]
        public string Configuration { get; set; }
        [Column("dot_net_cli_version")]
        public string DotNetCliVersion { get; set; }
        [Column("chronometer_frequency")]
        public ChronometerFrequency ChronometerFrequency { get; set; }
        [Column("hardware_timer_kind")]
        public string HardwareTimerKind { get; set; }
    }
}