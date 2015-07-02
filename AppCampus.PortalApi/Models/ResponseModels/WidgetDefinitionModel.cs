using AppCampus.Domain.Models.Entities;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The widget definition model.
    /// </summary>
    public class WidgetDefinitionModel
    {
        /// <summary>
        /// The identifier of the widget definition.
        /// </summary>
        public Guid WidgetDefinitionId { get; private set; }

        /// <summary>
        /// The name of the widget definition.
        /// </summary>
        public string Name { get; private set; }

        internal static WidgetDefinitionModel From(WidgetDefinition widgetDefinition)
        {
            return new WidgetDefinitionModel()
            {
                WidgetDefinitionId = widgetDefinition.Id,
                Name = widgetDefinition.Name
            };
        }
    }
}