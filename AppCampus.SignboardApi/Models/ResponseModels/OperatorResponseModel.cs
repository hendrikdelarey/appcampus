using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Operator model
    /// </summary>
    public class OperatorResponseModel
    {
        /// <summary>
        /// The Name of the Operator
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The IconId of the operator.
        /// The Id represents a reference to an image (logo of operator).
        /// </summary>
        public Guid IconId { get; set; }
    }
}