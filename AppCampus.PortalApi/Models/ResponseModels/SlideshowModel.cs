using AppCampus.Domain.Models.Entities;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The slideshow model.
    /// </summary>
    public class SlideshowModel
    {
        /// <summary>
        /// The identifier of the slideshow.
        /// </summary>
        public Guid SlideshowId { get; private set; }

        /// <summary>
        /// The name of the slideshow.
        /// </summary>
        public string Name { get; private set; }

        internal static SlideshowModel From(Slideshow slideshow)
        {
            return new SlideshowModel()
            {
                SlideshowId = slideshow.Id,
                Name = slideshow.Name
            };
        }
    }
}