using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidSignboardIdAttribute : ValidationAttribute
    {
        public ISignboardRepository SignboardRepository { get; set; }

        public ValidSignboardIdAttribute()
            : base("Signboard does not exist.")
        {
            SignboardRepository = (ISignboardRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ISignboardRepository));
        }

        public override bool IsValid(object value)
        {
            return SignboardRepository.Find((Guid)value) != null;
        }
    }
}