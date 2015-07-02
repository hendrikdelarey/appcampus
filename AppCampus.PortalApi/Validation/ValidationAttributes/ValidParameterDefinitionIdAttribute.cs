using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidParameterDefinitionIdAttribute : ValidationAttribute
    {
        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public ValidParameterDefinitionIdAttribute()
            : base("Not a valid ParameterDefintionId.")
        {
            WidgetDefinitionRepository = (IWidgetDefinitionRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(IWidgetDefinitionRepository));
        }

        public override bool IsValid(object value)
        {
            return WidgetDefinitionRepository.HasParameterDefinitionId(Guid.Parse(value.ToString()));
        }
    }
}