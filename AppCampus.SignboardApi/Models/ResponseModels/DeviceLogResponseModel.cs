using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// Device Log response model
    /// </summary>
    public class DeviceLogResponseModel
    {
        /// <summary>
        /// Identifier of the Log file
        /// </summary>
        public Guid LogFileId { get; set; }

        private DeviceLogResponseModel(Guid logFileId) 
        {
            LogFileId = logFileId;
        }

        public static DeviceLogResponseModel From(Guid logFileId) 
        {
            return new DeviceLogResponseModel(logFileId);
        }
    }
}