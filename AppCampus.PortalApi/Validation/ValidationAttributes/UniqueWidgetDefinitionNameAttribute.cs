using AppCampus.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class UniqueWidgetDefinitionNameAttribute : ValidationAttribute
    {
        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public UniqueWidgetDefinitionNameAttribute()
            : base("WidgetDefinition name must be unique.")
        {
            WidgetDefinitionRepository = (IWidgetDefinitionRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(IWidgetDefinitionRepository));
        }

        public override bool IsValid(object value)
        {
            return !WidgetDefinitionRepository.HasName(value.ToString().Trim());
        }
    }
}