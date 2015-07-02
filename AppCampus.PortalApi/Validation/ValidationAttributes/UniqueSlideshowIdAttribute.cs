using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class UniqueSlideshowIdAttribute : ValidationAttribute
    {
        public ISlideshowRepository SlideshowRepository { get; set; }

        public UniqueSlideshowIdAttribute()
            : base("Company Id does not exist.")
        {
            SlideshowRepository = (ISlideshowRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ISlideshowRepository));
        }

        public override bool IsValid(object value)
        {
            return SlideshowRepository.Find(Guid.Parse(value.ToString())) == null;
        }
    }
}