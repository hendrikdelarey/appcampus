using AppCampus.Domain.Models.Enums;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class DiagnosticMetric
    {
        public DiagnosticMetricType Metric { get; private set; }

        public string Value { get; private set; }

        public DiagnosticMetric(DiagnosticMetricType metric, string value)
        {
            Metric = metric;
            Value = value;
        }
    }
}