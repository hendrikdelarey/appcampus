using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Slideshow response model
    /// </summary>
    public class SlideshowResponseModel
    {
        /// <summary>
        /// List of Slides that will be displayed on the Slideshow
        /// </summary>
        public List<SlideResponseModel> Slides { get; set; }
    }
}