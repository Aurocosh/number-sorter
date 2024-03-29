﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NumberSorter.Domain.Benchmark.Data
{
    [Table("report_data")]
    internal sealed class ReportData
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("type")]
        public string Type { get; set; }
        [Column("checksum")]
        public string Checksum { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }

        [Column("host_environment_info_id")]
        public int HostEnvironmentInfoId { get; set; }
        [ForeignKey("HostEnvironmentInfoId")]
        public HostEnvironmentInfo HostEnvironmentInfo { get; set; }

        [ForeignKey("ReportId")]
        public List<BenchmarkResult> Benchmarks { get; set; }

        public void FillBlanks()
        {
            foreach (var benchmark in Benchmarks)
                benchmark.FillBlanks();
        }
    }
}
