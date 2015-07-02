using AppCampus.Resolution;
using AppCampus.SignboardApi.Filters;
using Microsoft.Practices.Unity;
using System.Web.Http;
using Drumble.Logging.WebApi.Filters;

namespace AppCampus.SignboardApi
{
    internal static class WebApiConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            var container = new UnityContainer();
            config.DependencyResolver = new UnityResolver(container);

            Resolver resolver = new Resolver();
            resolver.RegisterTypes(container);

            config.MapHttpAttributeRoutes();

            config.Filters.Add(new GenericExceptionFilterAttribute());

            config.Filters.Add(new LogAttribute("SignboardApi", 500));
        }
    }
}