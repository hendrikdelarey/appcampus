using Swashbuckle.Application;
using System.Web.Http;

namespace AppCampus.SignboardApi
{
    internal static class SwaggerConfig
    {
        internal static void Register(HttpConfiguration config)
        {
            config
               .EnableSwagger(c =>
               {
                   c.SingleApiVersion("v1", "AppCampus SignboardApi");
                   c.IncludeXmlComments(GetXmlCommentsPath());
               })
               .EnableSwaggerUi();
        }

        private static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}bin\AppCampus.SignboardApi.xml", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}