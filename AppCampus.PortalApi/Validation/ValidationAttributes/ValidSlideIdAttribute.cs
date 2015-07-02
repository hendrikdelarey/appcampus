using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidSlideIdAttribute : ValidationAttribute
    {
        public ISlideshowRepository SlideshowRepository { get; set; }

        public ValidSlideIdAttribute()
            : base("Slideshow does not exist.")
        {
            SlideshowRepository = (ISlideshowRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ISlideshowRepository));
        }

        public override bool IsValid(object value)
        {
            return SlideshowRepository.HasSlideId(Guid.Parse(value.ToString()));
        }
    }
}