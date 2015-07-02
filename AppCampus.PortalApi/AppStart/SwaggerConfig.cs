using Swashbuckle.Application;
using System;
using System.Web.Http;

namespace AppCampus.PortalApi
{
    internal static class SwaggerConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "AppCampus PortalApi");
                    c.IncludeXmlComments(GetXmlCommentsPath());
                })
                .EnableSwaggerUi();
        }

        private static string GetXmlCommentsPath()
        {
            return String.Format(@"{0}bin\AppCampus.PortalApi.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}