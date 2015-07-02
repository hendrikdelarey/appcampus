using AppCampus.PortalApi.Validation;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The widget definition input model.
    /// </summary>
    public class WidgetDefinitionInputModel
    {
        /// <summary>
        /// The name of the widget definition.
        /// </summary>
        [Required]
        [UniqueWidgetDefinitionName]
        public string Name { get; set; }
    }
}