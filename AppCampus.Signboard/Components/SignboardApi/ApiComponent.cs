using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.NetworkInterfacing;
using AppCampus.Signboard.Components.SignboardApi.Models;
using AppCampus.Signboard.Models;
using RestSharp;
using System;
using System.Net;

namespace AppCampus.Signboard.Components.SignboardApi
{
    public class ApiComponent
    {
        private RestClient client;

        public ApiComponent(Uri serverUrl)
        {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
            (se, cert, chain, sslerror) =>
            {
                return true;
            };

            client = new RestClient(serverUrl);
            client.Timeout = 60000;
        }

        public DeviceStateRequest GetState(MacAddress macAddress)
        {
            var request = new RestRequest("v1/state/{macAddress}", Method.GET)
            {
                RequestFormat = RestSharp.DataFormat.Json
            };

            request.AddParameter("macAddress", macAddress.Address);

            var response = client.Execute<StateModel>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return DeviceStateRequest.From((DeviceState)Enum.Parse(typeof(DeviceState), response.Data.State), response.Data.ChangeDate, response.Data.Comment);
            }
            else if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                Logger.Instance.Write("ApiComponent.GetState", LogLevel.Medium, String.Format("Resource '{0}' responded with unexpected HTTP code '{1}'. Message: {2}", request.Resource, response.StatusCode, response.Data.Comment));
                throw new InvalidOperationException(response.Data.Comment);
            }
            else if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                Logger.Instance.Write("ApiComponent.GetState", LogLevel.Medium, String.Format("Resource '{0}' responded with unexpected HTTP code '{1}'. Content: {2}", request.Resource, response.StatusCode, response.Content));
                throw new InvalidOperationException(String.Format("Unexpected error: {0}", response.Content));
            }
            else
            {
                Logger.Instance.Write("ApiComponent.GetState", LogLevel.Medium, String.Format("Resource '{0}' experienced an unexpected error: {1}", request.Resource, response.ErrorMessage));
                throw new InvalidOperationException(String.Format("Unexpected error: {0}", response.ErrorMessage));
            }
        }

        public DeviceStateRequest RequestApproval(Guid companyId, MacAddress macAddress, string comment)
        {
            var request = new RestRequest(String.Format("v1/state/{0}/pending", macAddress.Address), Method.POST)
            {
                RequestFormat = RestSharp.DataFormat.Json
            };

            request.AddBody(new { CompanyId = companyId, Comment = comment });

            var response = client.Execute<StateModel>(request);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return DeviceStateRequest.From((DeviceState)Enum.Parse(typeof(DeviceState), response.Data.State), response.Data.ChangeDate, comment);
            }
            else if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                Logger.Instance.Write("ApiComponent.RequestApproval", LogLevel.Medium, String.Format("Resource '{0}' responded with unexpected HTTP code '{1}'. Message: {2}", request.Resource, response.StatusCode, response.Data.Comment));
                throw new InvalidOperationException(response.Data.Comment);
            }
            else
            {
                Logger.Instance.Write("ApiComponent.RequestApproval", LogLevel.Medium, String.Format("Resource '{0}' experienced an unexpected error: {1}", request.Resource, response.ErrorMessage));
                throw new InvalidOperationException(String.Format("Unexpected error. {0}", response.Data.Comment));
            }
        }
    }
}