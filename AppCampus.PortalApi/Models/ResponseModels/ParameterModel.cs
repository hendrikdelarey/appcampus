using AppCampus.Domain.Models.ValueObjects;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The parameter model.
    /// </summary>
    public class ParameterModel
    {
        /// <summary>
        /// The parameter model.
        /// </summary>
        public Guid ParameterDefinitionId { get; private set; }

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        public string Value { get; private set; }

        internal static ParameterModel From(Parameter parameter)
        {
            return new ParameterModel()
            {
                ParameterDefinitionId = parameter.ParameterDefinitionId,
                Value = parameter.Value
            };
        }
    }
}