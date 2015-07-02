using AppCampus.Domain.Models.Entities;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The widget model.
    /// </summary>
    public class WidgetModel
    {
        /// <summary>
        /// The identifier of the widget.
        /// </summary>
        public Guid WidgetId { get; private set; }

        /// <summary>
        /// The identifier of the widget's widget definition.
        /// </summary>
        public Guid WidgetDefinitionId { get; private set; }

        internal static WidgetModel From(Widget widget)
        {
            return new WidgetModel()
            {
                WidgetId = widget.Id,
                WidgetDefinitionId = widget.WidgetDefinitionId
            };
        }
    }
}