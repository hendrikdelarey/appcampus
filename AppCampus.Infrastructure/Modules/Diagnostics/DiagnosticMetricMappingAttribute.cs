using AppCampus.Domain.Models.Enums;
using System;

namespace AppCampus.Infrastructure.Modules.Diagnostics
{
    public sealed class DiagnosticMetricMappingAttribute : Attribute
    {
        public DiagnosticMetricType Metric { get; private set; }

        public DiagnosticMetricMappingAttribute(DiagnosticMetricType metric)
        {
            Metric = metric;
        }
    }
}