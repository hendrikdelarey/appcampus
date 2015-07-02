using AppCampus.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class UniqueParameterDefinitionNameAttribute : ValidationAttribute
    {
        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public UniqueParameterDefinitionNameAttribute()
            : base("ParameterDefinition name must be unique.")
        {
            WidgetDefinitionRepository = (IWidgetDefinitionRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(IWidgetDefinitionRepository));
        }

        public override bool IsValid(object value)
        {
            return !WidgetDefinitionRepository.HasParameterDefinitionName(value.ToString().Trim());
        }
    }
}