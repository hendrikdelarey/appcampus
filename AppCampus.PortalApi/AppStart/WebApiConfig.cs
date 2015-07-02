using AppCampus.Domain.Interfaces.Components;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Validation.RouteConstraints;
using AppCampus.Resolution;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Routing;
using Drumble.Logging.WebApi.Filters;

namespace AppCampus.PortalApi
{
    internal static class WebApiConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            // IOC container
            var container = new UnityContainer();
            config.DependencyResolver = new UnityResolver(container);

            // IOC resolution
            Resolver resolver = new Resolver();
            resolver.RegisterTypes(container);

            // Logger
            var logger = container.Resolve<ILoggingComponent>();
            logger.LogInfo(MethodBase.GetCurrentMethod(), "Web Api configuration started.");

            //var cors = new EnableCorsAttribute("localhost:50937,localhost:80", "*", "*");
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Ignore any authentication which happens before the Web API pipeline (by ISS or OWIN).  This restricts the API to only authenticate using bearer tokens.
            config.SuppressDefaultHostAuthentication();

            // Enable authentication using bearer tokens
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Custom route constraints
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("deviceState", typeof(DeviceStateRouteConstraint));
            constraintResolver.ConstraintMap.Add("companyId", typeof(CompanyIdRouteConstraint));
            constraintResolver.ConstraintMap.Add("macAddress", typeof(MacAddressRouteConstraint));

            // API attribute routing
            config.MapHttpAttributeRoutes(constraintResolver);

            // API action filters
            config.Filters.Add(new LogAttribute("PortalApi", 200));
            config.Filters.Add(new ValidateModelStateAttribute());
            config.Filters.Add(new GlobalExceptionFilterAttribute(logger));

            // API formatters
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

            logger.LogInfo(MethodBase.GetCurrentMethod(), "Web Api configuration complete.");
        }
    }
}