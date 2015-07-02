using AppCampus.Signboard.Components.Diagnostics.Readers;
using AppCampus.Signboard.Models.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AppCampus.Signboard.Components.Diagnostics
{
    public class DiagnosticsComponent : IDisposable
    {
        private object locker;

        protected IDictionary<IDiagnosticReader, int> Readings { get; private set; }

        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        public DiagnosticsComponent(int pollingIntervalInSeconds)
        {
            locker = new object();

            Readings = new Dictionary<IDiagnosticReader, int>();
            Readings.Add(new CpuUsagePercentageDiagnosticReader(), 0);
            Readings.Add(new RamUsagePercentageDiagnosticReader(), 0);
            Readings.Add(new DiskUsagePercentageDiagnosticReader(), 0);
            Readings.Add(new NetworkTrafficBytesReceivedDiagnosticsReader(), 0);
            Readings.Add(new NetworkTrafficBytesSentDiagnosticsReader(), 0);

            CancellationTokenSource = new CancellationTokenSource();

            Task.Factory.StartNew(() =>
            {
                while (!CancellationTokenSource.IsCancellationRequested)
                {
                    foreach (var diagnosticReader in Readings.Keys.ToList())
                    {
                        lock(locker)
                        {
                            Readings[diagnosticReader] = diagnosticReader.ReadDiagnostic();
                        }
                    }

                    Thread.Sleep(pollingIntervalInSeconds);
                }

                Readings.Keys.ToList().ForEach(k => k.Dispose());
            }, CancellationTokenSource.Token);
        }

        public int GetCpuUsagePercentage()
        {
            lock (locker)
            {
                return Readings.Single(x => x.Key is CpuUsagePercentageDiagnosticReader).Value;
            }
        }

        public int GetRamUsagePercentage()
        {
            lock (locker)
            {
                return Readings.Single(x => x.Key is RamUsagePercentageDiagnosticReader).Value;
            }
        }

        public int GetDiskUsagePercentage()
        {
            lock (locker)
            {
                return Readings.Single(x => x.Key is DiskUsagePercentageDiagnosticReader).Value;
            }
        }

        public int GetNetworkTrafficBytesRecieved() 
        {
            lock (locker)
            {
                return Readings.Single(x => x.Key is NetworkTrafficBytesReceivedDiagnosticsReader).Value;
            }
        }

        public int GetNetworkTrafficBytesSent()
        {
            lock (locker)
            {
                return Readings.Single(x => x.Key is NetworkTrafficBytesSentDiagnosticsReader).Value;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                CancellationTokenSource.Cancel();
            }
        }

        public List<DiagnosticMetricModel> GetMetrics() 
        {
            List<DiagnosticMetricModel> metrics = new List<DiagnosticMetricModel>();
            metrics.Add(DiagnosticMetricModel.From("DiskUsagePercentage", GetDiskUsagePercentage()));
            metrics.Add(DiagnosticMetricModel.From("RamUsagePercentage", GetRamUsagePercentage()));
            metrics.Add(DiagnosticMetricModel.From("CpuUsagePercentage", GetCpuUsagePercentage()));
            metrics.Add(DiagnosticMetricModel.From("NetworkTrafficBytesRecieved", GetNetworkTrafficBytesRecieved()));
            metrics.Add(DiagnosticMetricModel.From("NetworkTrafficBytesSent", GetNetworkTrafficBytesSent()));

            return metrics;
        }
    }
}