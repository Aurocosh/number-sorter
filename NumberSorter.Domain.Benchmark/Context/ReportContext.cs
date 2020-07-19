using NumberSorter.Domain.Benchmark.Data;
using System.Data.Entity;

namespace NumberSorter.Domain.Benchmark.Context
{
    internal sealed class ReportContext : DbContext
    {
        public ReportContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        public DbSet<HostEnvironmentInfo> EnvironmentInfos { get; set; }
        public DbSet<ReportData> ReportDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }
}
