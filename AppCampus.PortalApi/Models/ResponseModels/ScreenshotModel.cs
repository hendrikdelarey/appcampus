using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The screenshot response model.
    /// </summary>
    public class ScreenshotModel
    {
        /// <summary>
        /// The unique identifier of the screenshot
        /// </summary>
        public Guid ScreenshotId { get; set; }

        /// <summary>
        /// The base 64 string representing the image
        /// </summary>
        public string Base64ImageString { get; set; }

        /// <summary>
        /// The date and time the screenshot was created.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        public static ScreenshotModel From(Screenshot screenshot) 
        {
            return new ScreenshotModel()
            {
                ScreenshotId = screenshot.Id,
                Base64ImageString = screenshot.Base64ImageString,
                CreatedDate = screenshot.CreatedDate
            };
        }
    }
}