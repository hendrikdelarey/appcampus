using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Widget : DomainEntity<Guid>
    {
        public Guid WidgetDefinitionId { get; private set; }

        public WidgetLayout Position { get; private set; }

        private List<Parameter> parameters;

        public IReadOnlyCollection<Parameter> Parameters
        {
            get
            {
                return parameters;
            }
        }

        public Widget(Guid widgetDefinitionId, WidgetLayout position)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            WidgetDefinitionId = widgetDefinitionId;
            parameters = new List<Parameter>();
            SetPosition(position);
        }

        public Widget(Guid id, Guid widgetDefinitionId, WidgetLayout position)
            : base(id)
        {
            WidgetDefinitionId = widgetDefinitionId;
            parameters = new List<Parameter>();
            SetPosition(position);
        }

        public void AssignParameter(Parameter parameter)
        {
            var p = parameters.SingleOrDefault(x => x.ParameterDefinitionId == parameter.ParameterDefinitionId);
            if (p != null)
            {
                parameters.Remove(p);
            }

            parameters.Add(parameter);
        }

        public void SetPosition(WidgetLayout position)
        {
            if (position == null)
            {
                throw new ArgumentNullException("position", "Position may not be null.");
            }
            else
            {
                Position = position;
            }
        }
    }
}