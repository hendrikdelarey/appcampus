using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace AppCampus.Infrastructure.Modules.Diagnostics
{
    public class DiagnosticsTableEntity : TableEntity
    {
        public Guid SignboardId { get; set; }

        public DateTime Date { get; set; }

        public string Version { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.CpuUsagePercentage)]
        public string CpuPercentageMetric { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.RamUsagePercentage)]
        public string RamPercentageMetric { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.DiskUsagePercentage)]
        public string DiskPercentageMetric { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.NetworkTrafficBytesRecieved)]
        public string NetworkTrafficBytesRecieved { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.NetworkTrafficBytesSent)]
        public string NetworkTrafficBytesSent { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.SignboardState)]
        public string SignboardStateMetric { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.IsShowingScreensaver)]
        public string IsShowingScreensaverMetric { get; set; }

        [DiagnosticMetricMapping(DiagnosticMetricType.SlideshowHealthMetric)]
        public string SlideshowObject { get; set; }

        public DiagnosticsTableEntity()
        {
        }

        public DiagnosticsTableEntity(Guid signboardId, DateTime date, SoftwareVersion version)
        {
            PartitionKey = signboardId.ToString();

            RowKey = String.Format("{0:D19}", (DateTime.MaxValue - date.ToUniversalTime()).Ticks);

            Date = date;
            SignboardId = signboardId;
            Version = version.VersionNumber;
        }
    }
}