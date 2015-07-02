
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// The Diagnostic Input Model
    /// </summary>
    public class DiagnosticInputModel
    {
        /// <summary>
        /// The Date of the diagnostic
        /// </summary>
        [Required]
        public DateTime DiagnosticDate { get; set; }

        /// <summary>
        /// The Software version installed on the Signboard
        /// </summary>
        [Required]
        public string SoftwareVersion { get; set; }

        /// <summary>
        /// The List of Metrics and values
        /// </summary>
        public List<DiagnosticMetricInputModel> Metrics { get; set; }

        /// <summary>
        /// The object containing the Health states of the screen
        /// </summary>
        public SignboardHealthModel SignboardHealth { get; set; }

    }
}