using AppCampus.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AppCampus.PortalApi.Validation.RouteConstraints
{
    public class DeviceStateRouteConstraint : IHttpRouteConstraint
    {
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                DeviceState result;
                return Enum.TryParse<DeviceState>(value.ToString(), true, out result);
            }
            else
            {
                return false;
            }
        }
    }
}