using AppCampus.Domain.Models.ValueObjects;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AppCampus.PortalApi.Validation.RouteConstraints
{
    public class MacAddressRouteConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                return MacAddress.IsValid(value.ToString());
            }
            else
            {
                return false;
            }
        }
    }
}