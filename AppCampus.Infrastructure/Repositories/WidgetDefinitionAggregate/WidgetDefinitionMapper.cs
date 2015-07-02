using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Infrastructure.Models;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.CompanyAggregate
{
    public class WidgetDefinitionMapper : IEntityMapper<WidgetDefinition, WidgetDefinitionTable>
    {
        public WidgetDefinition CreateFrom(WidgetDefinitionTable dataEntity)
        {
            var widgetDefinition = new WidgetDefinition(dataEntity.Id, dataEntity.Name);
            foreach (ParameterDefinitionTable parameterDefinitionTable in dataEntity.ParameterDefinitions)
            {
                var newDefinition = new ParameterDefinition(
                    parameterDefinitionTable.Id,
                    parameterDefinitionTable.Name,
                    parameterDefinitionTable.DefaultValue,
                    (ParameterDefinitionType)parameterDefinitionTable.ParameterType);
                widgetDefinition.AddParameterDefinition(newDefinition);
            }

            return widgetDefinition;
        }

        public WidgetDefinitionTable CreateFrom(WidgetDefinition domainEntity)
        {
            return new WidgetDefinitionTable()
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name,
                AssemblyType = domainEntity.AssemblyType,
                ParameterDefinitions = domainEntity.ParameterDefinitions.Select(parameterDefinition =>
                    new ParameterDefinitionTable
                    {
                        Name = parameterDefinition.Name,
                        Id = parameterDefinition.Id,
                        WidgetDefinitionId = domainEntity.Id,
                        DefaultValue = parameterDefinition.DefaultValue,
                        ParameterType = (int)parameterDefinition.DefinitionType
                    }).ToList()
            };
        }
    }
}