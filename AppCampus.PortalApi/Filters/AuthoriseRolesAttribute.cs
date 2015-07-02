using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace AppCampus.PortalApi.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public sealed class AuthoriseRolesAttribute : AuthorizeAttribute
    {
        public ICollection<RoleClassification> RoleClasses { get; private set; }

        public AuthoriseRolesAttribute(params RoleClassification[] roleClasses)
        {
            RoleClasses = roleClasses.ToList();
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return actionContext.RequestContext.Principal.Identity.IsAuthenticated && (RoleClasses.Select(x => x.ToString()).Any(actionContext.RequestContext.Principal.IsInRole) || (actionContext.RequestContext.Principal.Identity as ClaimsIdentity).HasClaim("isSuper", "true"));
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if (actionContext.RequestContext.Principal.Identity.IsAuthenticated)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden, ErrorModel.FromUnauthorised());
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, ErrorModel.FromUnauthenticated());
            }
        }
    }
}