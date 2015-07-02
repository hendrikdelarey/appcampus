using System;

namespace AppCampus.PortalApi.Models.ActionModels
{
    /// <summary>
    /// The slideshow action model.
    /// </summary>
    public class SlideshowUpdateActionModel
    {
        /// <summary>
        /// The action you want to perform.
        /// </summary>
        public SlideshowOperation Operation { get; set; }

        /// <summary>
        /// The identifier of the slide.
        /// </summary>
        public Guid SlideId { get; set; }

        /// <summary>
        /// The identifier of the referenced slide.
        /// </summary>
        public Guid ReferencedSlideId { get; set; }

        public enum SlideshowOperation { MoveAfter, MoveBefore }
    }
}