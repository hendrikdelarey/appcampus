using AppCampus.Domain.Models.Entities;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The slide model.
    /// </summary>
    public class SlideModel
    {
        /// <summary>
        /// The identifier of the slide.
        /// </summary>
        public Guid SlideId { get; private set; }

        /// <summary>
        /// The name of the slide.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The default background colour of the slide.
        /// </summary>
        public string BackgroundColourHexCode { get; private set; }

        /// <summary>
        /// The type of transition when introducing the slide in the slideshow.
        /// </summary>
        public string TransitionType { get; private set; }

        /// <summary>
        /// The length of time to display the slide in the slideshow.
        /// </summary>
        public int DurationInSeconds { get; private set; }

        /// <summary>
        /// Slide will only appear on slideshow if set to true.
        /// </summary>
        public bool IsActive { get; private set; }

        internal static SlideModel From(Slide slide)
        {
            return new SlideModel()
            {
                SlideId = slide.Id,
                BackgroundColourHexCode = slide.BackgroundColour.ColourHex,
                TransitionType = slide.Transition.Type.ToString(),
                DurationInSeconds = slide.Duration.Seconds,
                Name = slide.Name,
                IsActive = slide.IsActive
            };
        }
    }
}