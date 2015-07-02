using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The software input model.
    /// </summary>
    public class SoftwareInputModel
    {
        /// <summary>
        /// The version of the software.
        /// </summary>
        [Required]
        public string Version { get; set; }

        /// <summary>
        /// A comment describing the version of the software.
        /// </summary>
        public string Comment { get; set; }
    }
}