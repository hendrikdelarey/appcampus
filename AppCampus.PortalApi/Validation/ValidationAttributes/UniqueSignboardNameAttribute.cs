using AppCampus.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation.ValidationAttributes
{
    public sealed class UniqueSignboardNameAttribute : ValidationAttribute
    {
        public ISignboardRepository SignboardRepository { get; set; }

        public UniqueSignboardNameAttribute()
            : base("Signboard name must be unique.")
        {
            SignboardRepository = (ISignboardRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ISignboardRepository));
        }

        public override bool IsValid(object value)
        {
            return !SignboardRepository.HasName(value.ToString().Trim());
        }
    }
}