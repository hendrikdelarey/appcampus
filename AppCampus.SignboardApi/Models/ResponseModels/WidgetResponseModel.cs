using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Widget Response Model
    /// </summary>
    public class WidgetResponseModel
    {
        /// <summary>
        /// The Widget Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The Widget Position Model
        /// </summary>
        public WidgetPositionResponseModel Position { get; set; }

        /// <summary>
        /// The List of Parameters associated with the Widget
        /// </summary>
        public List<ParameterResponseModel> Parameters { get; set; }
    }
}