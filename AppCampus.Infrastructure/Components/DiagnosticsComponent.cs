using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Modules.Diagnostics;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Infrastructure.Components
{
    public class DiagnosticsComponent : IDiagnosticsComponent
    {
        private DiagnosticsTableStorage diagnosticsTableStorage;

        protected DiagnosticsTableStorage DiagnosticsTableStorage
        {
            get
            {
                if (diagnosticsTableStorage == null)
                {
                    var connectionString = CloudConfigurationManager.GetSetting("DefaultStorageAccount");

                    diagnosticsTableStorage = new DiagnosticsTableStorage(connectionString, "appcampussignboarddiagnostics");
                }

                return diagnosticsTableStorage;
            }
        }

        public DiagnosticsComponent()
        {
        }

        public SignboardDiagnostics GetLatest(Guid signboardId)
        {
            var tableEntity = DiagnosticsTableStorage.ReadLatest(signboardId);

            return DiagnosticsComponent.Map(tableEntity);
        }

        public IEnumerable<SignboardDiagnostics> GetFrom(Guid signboardId, DateTime fromDate, int take)
        {
            var entities = DiagnosticsTableStorage.ReadFrom(signboardId, fromDate, take);

            return entities.Select(x => DiagnosticsComponent.Map(x));
        }

        public void Write(SignboardDiagnostics diagnostics)
        {
            DiagnosticsTableStorage.Write(DiagnosticsComponent.Map(diagnostics));
        }

        private static SignboardDiagnostics Map(DiagnosticsTableEntity tableEntity)
        {
            if (tableEntity == null)
            {
                return null;
            }

            var diagnostics = new SignboardDiagnostics(tableEntity.SignboardId, tableEntity.Date, SoftwareVersion.From(tableEntity.Version));

            diagnostics.AddMetricValue(DiagnosticMetricType.CpuUsagePercentage, tableEntity.CpuPercentageMetric);
            diagnostics.AddMetricValue(DiagnosticMetricType.RamUsagePercentage, tableEntity.RamPercentageMetric);
            diagnostics.AddMetricValue(DiagnosticMetricType.DiskUsagePercentage, tableEntity.DiskPercentageMetric);
            diagnostics.AddMetricValue(DiagnosticMetricType.SignboardState, tableEntity.SignboardStateMetric);
            diagnostics.AddMetricValue(DiagnosticMetricType.IsShowingScreensaver, tableEntity.IsShowingScreensaverMetric);
            diagnostics.AddMetricValue(DiagnosticMetricType.SlideshowHealthMetric, tableEntity.SlideshowObject);
            diagnostics.AddMetricValue(DiagnosticMetricType.NetworkTrafficBytesRecieved, tableEntity.NetworkTrafficBytesRecieved);
            diagnostics.AddMetricValue(DiagnosticMetricType.NetworkTrafficBytesSent, tableEntity.NetworkTrafficBytesSent);

            return diagnostics;
        }

        private static DiagnosticsTableEntity Map(SignboardDiagnostics entity)
        {
            if (entity == null)
            {
                return null;
            }

            var tableEntity = new DiagnosticsTableEntity(entity.SignboardId, entity.Date, entity.SoftwareVersion);

            tableEntity.CpuPercentageMetric = entity.GetMetricValue(DiagnosticMetricType.CpuUsagePercentage);
            tableEntity.RamPercentageMetric = entity.GetMetricValue(DiagnosticMetricType.RamUsagePercentage);
            tableEntity.DiskPercentageMetric = entity.GetMetricValue(DiagnosticMetricType.DiskUsagePercentage);
            tableEntity.SignboardStateMetric = entity.GetMetricValue(DiagnosticMetricType.SignboardState);
            tableEntity.IsShowingScreensaverMetric = entity.GetMetricValue(DiagnosticMetricType.IsShowingScreensaver);
            tableEntity.SlideshowObject = entity.GetMetricValue(DiagnosticMetricType.SlideshowHealthMetric);
            tableEntity.NetworkTrafficBytesRecieved = entity.GetMetricValue(DiagnosticMetricType.NetworkTrafficBytesRecieved);
            tableEntity.NetworkTrafficBytesSent = entity.GetMetricValue(DiagnosticMetricType.NetworkTrafficBytesSent);

            return tableEntity;
        }
    }
}