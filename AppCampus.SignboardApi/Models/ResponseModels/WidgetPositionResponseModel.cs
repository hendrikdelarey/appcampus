using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Widget Position Model
    /// </summary>
    public class WidgetPositionResponseModel
    {
        /// <summary>
        /// The Start Column Weight
        /// Float 0.0 - 1.0
        /// </summary>
        public float StartColumnWeight { get; set; }

        /// <summary>
        /// The Start Row Weight
        /// Float 0.0 - 1.0
        /// </summary>
        public float StartRowWeight { get; set; }

        /// <summary>
        /// The End Column Weight
        /// Float 0.0 - 1.0
        /// </summary>
        public float EndColumnWeight { get; set; }

        /// <summary>
        /// The End Row Weight
        /// Float 0.0 - 1.0
        /// </summary>
        public float EndRowWeight { get; set; }
    }
}