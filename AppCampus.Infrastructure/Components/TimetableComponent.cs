using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.ValueObjects;
using Microsoft.Azure;
using Microsoft.WindowsAzure;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Infrastructure.Components
{
    public class TimetableComponent : ITimetableComponent
    {
        private RestClient client;

        public TimetableComponent()
        {
            client = new RestClient(new Uri(CloudConfigurationManager.GetSetting("DrumbleGatewayUrl")));
        }

        private class TimetableScheduleResponseModel
        {
            public string FirstStop { get; set; }

            public string LastStop { get; set; }

            public string CorridorName { get; set; }

            public string VehicleNumber { get; set; }

            public string Status { get; set; }

            public DateTime StopArrivalTime { get; set; }

            public DateTime DepartureTime { get; set; }

            public DateTime DestinationArrivalTime { get; set; }
        }

        private class TimetableResponseModel
        {
            public List<TimetableScheduleResponseModel> Schedule { get; set; }
        }

        public IEnumerable<TimetableSchedule> GetTimetableSchedule(string operatorName, string stopCode, int numResults = 0)
        {
            var request = new RestRequest("v1/StopSchedule", Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddQueryParameter("CompanyName", operatorName);
            request.AddQueryParameter("StopCode", stopCode);
            if (numResults > 0) 
            { 
                request.AddQueryParameter("NumResults", numResults.ToString());
            }

            request.AddHeader("AppKey", CloudConfigurationManager.GetSetting("DrumbleGatewayApiToken"));

            var response = client.Execute<TimetableResponseModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data.Schedule.Select(x => TimetableSchedule.From(x.FirstStop, x.LastStop, x.CorridorName, x.VehicleNumber, x.Status, x.StopArrivalTime, x.DepartureTime, x.DestinationArrivalTime));
            }
            else
            {
                throw new InvalidOperationException(response.Content);
            }
        }
    }
}