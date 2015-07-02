using Microsoft.Owin;
using Microsoft.Owin.Extensions;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(AppCampus.SignboardApi.Startup))]

namespace AppCampus.SignboardApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            WebApiConfig.Register(config);
            SwaggerConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}