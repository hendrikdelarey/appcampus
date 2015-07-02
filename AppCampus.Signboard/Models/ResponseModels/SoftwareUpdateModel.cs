using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.ResponseModels
{
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

        internal static SoftwareModel From(Guid softwareId, string version)
        {
            return new SoftwareModel()
            {
                SoftwareId = softwareId,
                Version = version
            };
        }
    }
}
