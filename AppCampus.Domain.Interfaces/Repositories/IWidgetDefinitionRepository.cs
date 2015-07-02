using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface IWidgetDefinitionRepository : IRepository<WidgetDefinition, Guid>
    {
        IReadOnlyCollection<WidgetDefinition> GetAll();

        bool HasName(string name);

        bool HasParameterDefinitionName(string name);

        bool HasParameterDefinitionId(Guid parameterDefinitionId);

        WidgetDefinition FindByName(string definitionName);
    }
}