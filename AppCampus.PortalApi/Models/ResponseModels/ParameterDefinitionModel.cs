using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The parameter definition model.
    /// </summary>
    public class ParameterDefinitionModel
    {
        /// <summary>
        /// The identifier of the parameter definition.
        /// </summary>
        public Guid ParameterDefinitionId { get; private set; }

        /// <summary>
        /// The name of the parameter definition.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The type of the parameter definition.
        /// </summary>
        public ParameterDefinitionType ParameterType { get; private set; }

        internal static ParameterDefinitionModel From(ParameterDefinition parameterDefinition)
        {
            return new ParameterDefinitionModel()
            {
                ParameterDefinitionId = parameterDefinition.Id,
                Name = parameterDefinition.Name,
                ParameterType = parameterDefinition.DefinitionType
            };
        }
    }
}