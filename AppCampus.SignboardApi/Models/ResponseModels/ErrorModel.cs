using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Error model.
    /// This gets returned when an unexpected error occurs on the server.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// The Message explaining the error.
        /// </summary>
        public string Message { get; set; }

        public static ErrorModel From(string message)
        {
            return new ErrorModel
            {
                Message = message
            };
        }

        internal static ErrorModel FromUnauthorised()
        {
            return new ErrorModel
            {
                Message = "Authorisation denied for this request."
            };
        }

        internal static ErrorModel FromUnauthenticated()
        {
            return new ErrorModel
            {
                Message = "Authentication failed."
            };
        }

        internal static ErrorModel FromNotFound()
        {
            return new ErrorModel
            {
                Message = "Resource not found or not available."
            };
        }

        internal static ErrorModel FromUnsupportedMediaType()
        {
            return new ErrorModel
            {
                Message = "Media type not supported."
            };
        }
    }
}