using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The signboard diagnostic model.
    /// </summary>
    public class SignboardDiagnosticModel
    {
        /// <summary>
        /// The date of capture.
        /// </summary>
        public DateTime DiagnosticDate { get; private set; }

        /// <summary>
        /// The version of software hosted on the device at the date of capture.
        /// </summary>
        public string SoftwareVersion { get; private set; }

        /// <summary>
        /// The diagnostic metrics at the date of capture.
        /// </summary>
        public ICollection<SignboardDiagnosticMetricModel> Metrics { get; private set; }

        internal static SignboardDiagnosticModel From(SignboardDiagnostics diagnostics)
        {
            return new SignboardDiagnosticModel()
            {
                DiagnosticDate = diagnostics.Date,
                SoftwareVersion = diagnostics.SoftwareVersion.VersionNumber,
                Metrics = diagnostics.Metrics.Select(x => SignboardDiagnosticMetricModel.From(x.Metric.ToString(), x.Value)).ToList()
            };
        }
    }
}