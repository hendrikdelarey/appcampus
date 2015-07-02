using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// A Diagnostic Metric
    /// </summary>
    public class DiagnosticMetricInputModel
    {
        /// <summary>
        /// The Name of the Metric
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The Value of the Metric
        /// </summary>
        [Required]
        public int Value { get; set; }
    }
}