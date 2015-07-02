using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components
{
    public class ImageComponent : GatewayComponent
    {
        public ImageComponent() : 
            base()
        {
        }

        public string GetBase64Image(Guid imageId)
        {
            var request = new RestRequest("v1/widgetcontent/images/{imageId}", Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("imageId", imageId.ToString());

            var response = Client.Execute<ImageModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data.Base64Image;
            }
            else
            {
                Logger.Instance.Write("GetBase64Image", LogLevel.Medium, "Call failed with status code " + response.StatusCode);
                return null;
            }
        }
    }
}
