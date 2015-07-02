using System;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Parameter : IValueObject<Parameter>
    {
        public Guid ParameterDefinitionId { get; private set; }

        public string Value { get; private set; }

        private Parameter(Guid parameterDefinitionId, string value)
        {
            if (Parameter.IsValid(parameterDefinitionId, value))
            {
                Value = value;
                ParameterDefinitionId = parameterDefinitionId;
            }
            else
            {
                throw new ArgumentNullException("parameterDefinitionId", String.Format("Supplied parameterDefinitionId = {0} and value = {1} is not a valid Parameter. No null or empty Strings are allowed.", parameterDefinitionId, value));
            }
        }

        public static bool IsValid(Guid parameterDefinitionId, string value)
        {
            return (!parameterDefinitionId.Equals(Guid.Empty) && !String.IsNullOrWhiteSpace(value));
        }

        public bool Equals(Parameter other)
        {
            return (ParameterDefinitionId.Equals(other.ParameterDefinitionId) && Value.Equals(other.Value));
        }

        public static Parameter From(Parameter parameter)
        {
            return new Parameter(parameter.ParameterDefinitionId, parameter.Value);
        }

        public static Parameter From(Guid parameterDefinitionId, string value)
        {
            return new Parameter(parameterDefinitionId, value);
        }
    }
}