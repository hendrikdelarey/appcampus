using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Parameter Response Model
    /// </summary>
    public class ParameterResponseModel
    {
        /// <summary>
        /// The Definition of the Parameter
        /// </summary>
        public string Definition { get; set; }

        /// <summary>
        /// The Value of the parameter
        /// </summary>
        public string Value { get; set; }
    }
}