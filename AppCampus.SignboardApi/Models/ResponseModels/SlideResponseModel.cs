using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Slide response model
    /// </summary>
    public class SlideResponseModel
    {
        /// <summary>
        /// The Background colour of the slide
        /// </summary>
        public string BackgroundColour { get; set; }

        /// <summary>
        /// The Duration of the slide. 
        /// Duration is in seconds.
        /// </summary>
        public int DurationInSeconds { get; set; }

        /// <summary>
        /// List of widgets displayed on the Slide.
        /// </summary>
        public List<WidgetResponseModel> Widgets { get; set; }
    }
}