using AppCampus.PortalApi.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The AssignedSignboard input model.
    /// </summary>
    public class ScheduledSlideshowModel
    {
        /// <summary>
        /// Indentifier of the ScheduledSlideshow
        /// </summary>
        [Required]
        public Guid ScheduledSlideshowId { get; set; }

        /// <summary>
        /// Indentifier of the Slideshow assigned to the Signboard
        /// </summary>
        [Required]
        [ValidSlideshowId]
        public Guid SlideshowId { get; set; }

        /// <summary>
        /// StartDate of the slideshow
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }
    }
}