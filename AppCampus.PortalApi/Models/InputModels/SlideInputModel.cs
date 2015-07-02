using AppCampus.PortalApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The slide input model.
    /// </summary>
    public class SlideInputModel
    {
        /// <summary>
        /// The default background colour of the slide.
        /// </summary>
        [Required]
        [ValidHexColour]
        public string BackgroundColourHexCode { get; set; }

        /// <summary>
        /// The type of transition when introducing the slide in the slideshow.
        /// </summary>
        [Required]
        public string TransitionType { get; set; }

        /// <summary>
        /// The length of time to display the slide in the slideshow.
        /// </summary>
        [Required]
        [ValidDurationInSeconds]
        public int DurationInSeconds { get; set; }

        /// <summary>
        /// The name of the slide.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Slide will only display when true.
        /// </summary>
        [Required]
        public bool IsActive { get; set; }
    }
}