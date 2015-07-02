using RestSharp;
using System;
using System.Configuration;

namespace AppCampus.Signboard.Components
{
    public abstract class GatewayComponent
    {
        private static Uri serverUri = new Uri(ConfigurationManager.AppSettings["SignboardApiUri"]);

        public RestClient Client { get; private set; }

        public GatewayComponent()
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };

            Client = new RestClient(serverUri);
        }
    }
}