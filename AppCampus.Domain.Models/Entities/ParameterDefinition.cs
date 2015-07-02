using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class ParameterDefinition : DomainEntity<Guid>
    {
        public string Name { get; private set; }

        public string DefaultValue { get; private set; }

        public ParameterDefinitionType DefinitionType { get; private set; }

        public ParameterDefinition(string name, string defaultValue = null, ParameterDefinitionType type = ParameterDefinitionType.Undefined)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "Parameter Name may not be null or empty.");
            }

            Name = name;
            DefaultValue = defaultValue;
            DefinitionType = type;
        }

        public ParameterDefinition(Guid id, string name, string defaultValue, ParameterDefinitionType type)
            : base(id)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException("name", "Parameter Name may not be null or empty.");
            }

            Name = name;
            DefaultValue = defaultValue;
            DefinitionType = type;
        }
    }
}