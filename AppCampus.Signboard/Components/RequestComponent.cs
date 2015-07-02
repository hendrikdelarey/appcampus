using AppCampus.Signboard.Models.InputModels;
using AppCampus.Signboard.Models.ResponseModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Components
{
    public class RequestComponent : GatewayComponent
    {
        private RequestComponent() :
            base() 
        {}

        public void FinishRequest(string macAddress, RequestInputModel model) 
        {
            var request = new RestRequest("v1/signboards/{macAddress}/requests", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("macAddress", macAddress);
            request.AddBody(model);

            var response = Client.Execute<RequestModel>(request);
        }

        public static RequestComponent GetInstance() 
        {
            return new RequestComponent();
        }
    }
}
