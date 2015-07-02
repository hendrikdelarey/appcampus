using AppCampus.Signboard.Components.Diagnostics;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Models;
using AppCampus.Signboard.Models.QueryModels;
using RestSharp;
using System;

namespace AppCampus.Signboard.Components
{
    public class SlideshowStructureComponent : GatewayComponent
    {
        private SlideshowStructureComponent() :
            base()
        {
        }

        public Structure GetSlideshowStructure(string macAddress, DiagnosticModel diagnosticModel)
        {
            var request = new RestRequest("v1/signboards/{macAddress}", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("macAddress", macAddress);
            request.AddBody(diagnosticModel);

            var response = Client.Execute<StructureModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var slideshowStructure = response.Data;

                return Structure.From(slideshowStructure);
            }
            else
            {
                Logging.Logger.Instance.Write("GetSlideshowStructure", Logging.LogLevel.Medium, "GetStructures Call failed. StatusCode: " + response.StatusCode);
                return null;
            }
        }

        public static SlideshowStructureComponent GetInstance()
        {
            return new SlideshowStructureComponent();
        }
    }
}