using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using NumberSorter.Domain.Benchmark.Data;
using NumberSorter.Domain.Benchmark.Context;
using NumberSorter.Domain.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NumberSorter.Domain.Benchmark.Benchmarks.Upload
{
    internal class DatabaseUploader
    {
        public void Upload()
        {
            string currentFolder = AppDomain.CurrentDomain.BaseDirectory;
            string reportsPath = Path.Combine(currentFolder, "BenchmarkDotNet.Artifacts", "results");
            string settingsFile = Path.Combine(currentFolder, "data_upload_settings.json");

            if (!File.Exists(settingsFile))
            {
                Console.WriteLine("No upload settings. Skipping step.");
                return;
            }

            var jsonSerializerSettings = new JsonSerializerSettings();
            var jsonFileSerializer = new JsonFileSerializer(jsonSerializerSettings, true);

            var settings = jsonFileSerializer.LoadFromJsonFile<UploadSettings>(settingsFile);
            if (settings is null)
            {
                Console.WriteLine("Failed to load settings.");
                return;
            }

            if (!settings.Enabled)
                return;

            if (!Directory.Exists(reportsPath))
            {
                Console.WriteLine("Report directory path is incorrect");
                return;
            }

            using (var context = new ReportContext(settings.Connection.ConnectionString))
            {
                foreach (var reportPath in Directory.GetFiles(reportsPath, "*.json", SearchOption.TopDirectoryOnly))
                {
                    var report = jsonFileSerializer.LoadFromJsonFile<ReportData>(reportPath);
                    if (report == null)
                        continue;

                    var fileInfo = new FileInfo(reportPath);
                    report.Created = fileInfo.CreationTime;
                    report.Title = fileInfo.Name;
                    report.Type = report.Benchmarks[0].Type;

                    using (var sha256 = SHA256.Create())
                    using (var stream = File.OpenRead(reportPath))
                    {
                        var hash = sha256.ComputeHash(stream);
                        var builder = new StringBuilder(hash.Length);
                        foreach (byte b in hash)
                            builder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
                        report.Checksum = builder.ToString();
                    }

                    var alreadyUploaded = context.ReportDatas.Any(x => x.Checksum == report.Checksum);
                    if (alreadyUploaded)
                        continue;

                    var info = report.HostEnvironmentInfo;
                    var existingInfo = context.EnvironmentInfos.FirstOrDefault(x =>
                        x.BenchmarkDotNetCaption == info.BenchmarkDotNetCaption &&
                        x.BenchmarkDotNetVersion == info.BenchmarkDotNetVersion &&
                        x.OsVersion == info.OsVersion &&
                        x.ProcessorName == info.ProcessorName &&
                        x.PhysicalProcessorCount == info.PhysicalProcessorCount &&
                        x.PhysicalCoreCount == info.PhysicalCoreCount &&
                        x.LogicalCoreCount == info.LogicalCoreCount &&
                        x.RuntimeVersion == info.RuntimeVersion &&
                        x.Architecture == info.Architecture &&
                        x.HasAttachedDebugger == info.HasAttachedDebugger &&
                        x.HasRyuJit == info.HasRyuJit &&
                        x.Configuration == info.Configuration &&
                        x.DotNetCliVersion == info.DotNetCliVersion &&
                        x.ChronometerFrequency.Hertz == info.ChronometerFrequency.Hertz &&
                        x.HardwareTimerKind == info.HardwareTimerKind);

                    if (existingInfo != null)
                        report.HostEnvironmentInfo = existingInfo;

                    report.FillBlanks();

                    context.ReportDatas.Add(report);
                    context.SaveChanges();
                    Console.WriteLine($"Uploaded report: {report.Title}.");
                }
            }

            Console.WriteLine("Report upload completed");
        }
    }
}
