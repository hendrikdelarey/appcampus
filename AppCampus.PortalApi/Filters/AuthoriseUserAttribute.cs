using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AppCampus.PortalApi.Filters
{
    public sealed class AuthoriseUserAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            var principal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            if (actionContext.ControllerContext.RouteData.Values.All(x => x.Key != "userId"))
            {
                return false;
            }

            var userId = actionContext.ControllerContext.RouteData.Values["userId"].ToString();

            return principal != null && principal.HasClaim(x => x.Type.Equals("userId") && x.Value.ToUpper().Equals(userId.ToUpper()));
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, ErrorModel.FromUnauthorised());
        }
    }
}