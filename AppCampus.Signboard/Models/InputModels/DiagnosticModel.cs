using AppCampus.Signboard.Components.Diagnostics;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AppCampus.Signboard.Models.QueryModels
{
    public class DiagnosticModel
    {
        public DateTime DiagnosticDate { get; private set; }

        public string SoftwareVersion { get; private set; }

        public List<DiagnosticMetricModel> Metrics { get; private set; }

        public SignboardHealth SignboardHealth { get; private set; }

        public static DiagnosticModel From(DiagnosticsComponent diagnosticsComponent, SignboardHealth signboardHealth)
        {
            return new DiagnosticModel()
            {
                DiagnosticDate = DateTime.UtcNow,
                SoftwareVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(),
                Metrics = diagnosticsComponent.GetMetrics(),
                SignboardHealth = signboardHealth
            };
        }
    }
}