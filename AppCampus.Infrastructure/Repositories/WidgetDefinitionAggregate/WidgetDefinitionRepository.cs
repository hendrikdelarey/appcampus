using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.CompanyAggregate
{
    public class WidgetDefinitionRepository : EntityFrameworkRepository<WidgetDefinition, Guid, WidgetDefinitionTable>, IWidgetDefinitionRepository
    {
        public WidgetDefinitionRepository(IUnitOfWork unitOfWork, IEntityMapper<WidgetDefinition, WidgetDefinitionTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
            RegisterUpdateMapping(x => x.OwnedCollection(w => w.ParameterDefinitions));
            RegisterQueryMapping(x => x.ParameterDefinitions);
        }

        public override WidgetDefinition Find(Guid id)
        {
            WidgetDefinitionTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Id == id);
            }

            if (entity == null)
            {
                return null;
            }
            else
            {
                return EntityMapper.CreateFrom(entity);
            }
        }

        public IReadOnlyCollection<WidgetDefinition> GetAll()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public WidgetDefinition FindByName(string definitionName)
        {
            if (!HasName(definitionName))
            {
                return null;
            }

            using (var context = NewContext())
            {
                return EntityMapper.CreateFrom(context
                    .Query()
                    .Single(x => x.Name.Equals(definitionName.Trim()))
                    );
            }
        }

        public bool HasName(string name)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.Name.Equals(name.Trim()));
            }
        }

        public bool HasParameterDefinitionName(string name)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.ParameterDefinitions.Any(y => y.Name.Equals(name.Trim())));
            }
        }

        public bool HasParameterDefinitionId(Guid parameterDefinitionId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.ParameterDefinitions.Any(y => y.Id == parameterDefinitionId));
            }
        }
    }
}