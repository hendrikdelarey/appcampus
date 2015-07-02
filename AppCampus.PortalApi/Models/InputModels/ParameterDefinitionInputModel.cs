using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The parameter definition input model.
    /// </summary>
    public class ParameterDefinitionInputModel
    {
        /// <summary>
        /// The name of the parameter definition.
        /// </summary>
        [UniqueParameterDefinitionName]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The default value of the parameter definition.
        /// </summary>
        public string DefaultValue { get; set; }

        /// <summary>
        /// The type of parameter specified
        /// </summary>
        [Required]
        public ParameterDefinitionType ParameterType { get; set; }
    }
}