using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(AppCampus.PortalApi.Startup))]

namespace AppCampus.PortalApi
{
    public partial class Startup
    {
        public static HttpConfiguration HttpConfiguration { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            HttpConfiguration = config;

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);

            ConfigureAuth(app, config);

            app.UseWebApi(config);
        }
    }
}