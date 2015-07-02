using AppCampus.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The parameter input model.
    /// </summary>
    public class ParameterInputModel
    {
        /// <summary>
        /// The name of the parameter's parameter definition.
        /// </summary>
        [Required]
        public string ParameterName { get; set; }

        /// <summary>
        /// The value of the parameter.
        /// </summary>
        [Required]
        public string Value { get; set; }


    }
}