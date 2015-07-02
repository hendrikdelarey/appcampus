using AppCampus.PortalApi.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The widget input model.
    /// </summary>
    public class WidgetInputModel
    {
        /// <summary>
        /// The identifier of the widget's widget definition.
        /// </summary>
        [Required]
        [ValidWidgetDefinitionId]
        public Guid WidgetDefinitionId { get; set; }
    }
}