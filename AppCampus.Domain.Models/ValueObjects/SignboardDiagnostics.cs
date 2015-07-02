using AppCampus.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class SignboardDiagnostics
    {
        public Guid SignboardId { get; private set; }

        public DateTime Date { get; private set; }

        public SoftwareVersion SoftwareVersion { get; private set; }

        public ICollection<DiagnosticMetric> Metrics { get; private set; }

        public SignboardDiagnostics(Guid signboardId, DateTime date, SoftwareVersion version)
        {
            SignboardId = signboardId;
            SoftwareVersion = version;
            Date = date;
            Metrics = new List<DiagnosticMetric>();
        }

        public void AddMetricValue(DiagnosticMetricType metric, string value)
        {
            Metrics.Add(new DiagnosticMetric(metric, value));
        }

        public string GetMetricValue(DiagnosticMetricType metric)
        {
            var returnValue = Metrics.SingleOrDefault(x => x.Metric == metric);
            return returnValue == null ? null : returnValue.Value;
        }
    }
}