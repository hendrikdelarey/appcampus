using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Structure response model
    /// </summary>
    public class StructureResponseModel
    {
        /// <summary>
        /// The Slideshow that is assigned to the Signboard
        /// </summary>
        public SlideshowResponseModel Slideshow { get; set; }

        /// <summary>
        /// List of announcements that are associated with the Signboard.
        /// </summary>
        public List<AnnouncementResponseModel> Announcements { get; set; }

        /// <summary>
        /// Amount of seconds before the Signboard polls the api again.
        /// </summary>
        public int PollingIntervalInSeconds { get; set; }

        /// <summary>
        /// A list of requests the signboard has to perform.
        /// </summary>
        public List<RequestResponseModel> Requests { get; set; }

        /// <summary>
        /// A boolean showing whether the screensaver should show on the signboard.
        /// </summary>
        public bool IsShowingScreensaver { get; set; }

        /// <summary>
        /// The factor is multiplied with the default font size on the signbaord.
        /// </summary>
        public float FontFactor { get; set; }
    }
}