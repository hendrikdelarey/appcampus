using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidWidgetDefinitionIdAttribute : ValidationAttribute
    {
        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public ValidWidgetDefinitionIdAttribute()
            : base("Not a valid WidgetDefinitionId.")
        {
            WidgetDefinitionRepository = (IWidgetDefinitionRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(IWidgetDefinitionRepository));
        }

        public override bool IsValid(object value)
        {
            return WidgetDefinitionRepository.Find(Guid.Parse(value.ToString())) != null;
        }
    }
}