using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.SignboardApi.Models.QueryModels
{
    /// <summary>
    /// The object containing the Health and states of the widget
    /// </summary>
    public class WidgetHealthModel
    {
        /// <summary>
        /// The state of the widget
        /// </summary>
        public string WidgetState { get; set; }

        /// <summary>
        /// The type of Widget
        /// </summary>
        public string WidgetType { get; set; }
    }
}
