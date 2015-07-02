using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// Software response model
    /// </summary>
    public class SoftwareModel
    {
        /// <summary>
        /// The identifier of the software.
        /// </summary>
        public Guid SoftwareId { get; set; }

        /// <summary>
        /// The version of the software.
        /// </summary>
        public string Version { get; private set; }

        internal static SoftwareModel From(Software software)
        {
            return new SoftwareModel()
            {
                SoftwareId = software.Id,
                Version = software.Version.ToString()
            };
        }
    }
}