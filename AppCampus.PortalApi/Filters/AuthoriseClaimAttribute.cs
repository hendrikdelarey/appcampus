using AppCampus.PortalApi.Models.ResponseModels;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AppCampus.PortalApi.Filters
{
    public sealed class AuthoriseClaimAttribute : AuthorizeAttribute
    {
        public string Type { get; private set; }

        public string Value { get; private set; }

        public AuthoriseClaimAttribute(string type, string value)
        {
            Type = type;
            Value = value;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            var claimsPrincipal = actionContext.RequestContext.Principal as ClaimsPrincipal;

            return claimsPrincipal.HasClaim(Type, Value);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, ErrorModel.FromUnauthorised());
        }
    }
}