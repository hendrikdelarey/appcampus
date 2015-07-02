using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace AppCampus.SignboardApi.Filters
{
    public class GenericExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            HttpResponseMessage responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
            responseMessage.Content = new ObjectContent(typeof(ErrorModel), ErrorModel.From("Unexpected internal server error"), new JsonMediaTypeFormatter());

            context.Response = responseMessage;
        }
    }
}