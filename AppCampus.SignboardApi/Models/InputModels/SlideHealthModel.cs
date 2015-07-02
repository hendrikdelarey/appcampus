using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// The object containing the Health of a slide
    /// </summary>
    public class SlideHealthModel
    {
        /// <summary>
        /// The index of the slide in the slideshow
        /// </summary>
        public int SlideIndex { get; set; }

        /// <summary>
        /// List of all widgets health in slide
        /// </summary>
        public List<WidgetHealthModel> WidgetHealth { get; set; }
    }
}
