using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation.ValidationAttributes
{
    public sealed class UniqueDeviceIdAttribute : ValidationAttribute
    {
        public ISignboardRepository SignboardRepository { get; set; }

        public UniqueDeviceIdAttribute()
            : base("Device is already assigned to an active signooard.")
        {
            SignboardRepository = (ISignboardRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ISignboardRepository));
        }

        public override bool IsValid(object value)
        {
            return !SignboardRepository.HasDeviceId(Guid.Parse(value.ToString()));
        }
    }
}