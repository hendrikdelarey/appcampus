using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The software response model.
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

        /// <summary>
        /// A comment describing the version of the software
        /// </summary>
        public string Comment { get; set; }

        internal static SoftwareModel From(Software software)
        {
            return new SoftwareModel()
            {
                SoftwareId = software.Id,
                Version = software.Version.ToString(),
                Comment = software.Comment
            };
        }
    }
}