using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http.Routing;

namespace AppCampus.PortalApi.Validation.RouteConstraints
{
    public class CompanyIdRouteConstraint : IHttpRouteConstraint
    {
        public ICompanyRepository CompanyRepository { get; private set; }

        public CompanyIdRouteConstraint()
        {
            CompanyRepository = (ICompanyRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ICompanyRepository));
        }

        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            object value;

            if (values.TryGetValue(parameterName, out value) && value != null)
            {
                Guid id;

                if (Guid.TryParse(value.ToString(), out id) && CompanyRepository.Find(id) != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}