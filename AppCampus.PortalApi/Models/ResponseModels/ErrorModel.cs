namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The error response model.
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// The message explaining the error.
        /// </summary>
        public string Message { get; set; }

        internal static ErrorModel From(string message)
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