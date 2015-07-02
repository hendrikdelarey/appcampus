using AppCampus.Domain.Interfaces.Components;
using AppCampus.PortalApi.Models.ResponseModels;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Filters;

namespace AppCampus.PortalApi.Filters
{
    public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public ILoggingComponent Logging { get; private set; }

        public GlobalExceptionFilterAttribute(ILoggingComponent logging)
        {
            Logging = logging;
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(HttpStatusCode.BadRequest, ErrorModel.From("Unexpected internal server error"));
            Logging.LogError(MethodBase.GetCurrentMethod(), "Unhandled exception occurred. Bad Request response delivered.", actionExecutedContext.Exception);
        }
    }
}