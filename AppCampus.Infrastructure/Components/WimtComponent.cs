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
    public class DrumbleComponent : IDrumbleComponent
    {
        private RestClient client;

        public DrumbleComponent()
        {
            client = new RestClient(new Uri(CloudConfigurationManager.GetSetting("DrumbleGatewayUrl")));
        }

        public IEnumerable<Stop> GetStops()
        {
            var request = new RestRequest("v1/Stops", Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("AppKey", CloudConfigurationManager.GetSetting("DrumbleGatewayApiToken"));

            var response = client.Execute<StopsResponseModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                List<Stop> returnList = new List<Stop>();
                foreach (StopResponseModel stop in response.Data.Stops)
                {
                    foreach (StopLocationResponseModel stopLocation in stop.StopLocations)
                    {
                        returnList.Add(Stop.From(stopLocation.Name, stopLocation.Code, stop.Operator, stop.Mode));
                    }
                }

                return returnList;
            }
            else
            {
                throw new InvalidOperationException(response.Content);
            }
        }

        public IEnumerable<Operator> GetOperators()
        {
            var request = new RestRequest("v1/Operators", Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddHeader("AppKey", CloudConfigurationManager.GetSetting("DrumbleGatewayApiToken"));

            var response = client.Execute<OperatorsResponseModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Data.Operators.Select(x => Operator.From(x.Name, x.DisplayName, x.Modes, x.Category));
            }
            else
            {
                throw new InvalidOperationException(response.Content);
            }
        }

        private class OperatorsResponseModel
        {
            public List<OperatorResponseModel> Operators { get; set; }
        }

        private class OperatorResponseModel
        {
            public string Name { get; set; }

            public string DisplayName { get; set; }

            public List<string> Modes { get; set; }

            public string Category { get; set; }
        }

        private class StopsResponseModel
        {
            public List<StopResponseModel> Stops { get; set; }
        }

        private class StopResponseModel
        {
            public string Name { get; set; }

            public string Operator { get; set; }

            public string Mode { get; set; }

            public List<StopLocationResponseModel> StopLocations { get; set; }
        }

        private class StopLocationResponseModel
        {
            public string Name { get; set; }

            public string Code { get; set; }

            public string Address { get; set; }

            private Point Point { get; set; }
        }

        private class Point
        {
            public float Latitude { get; set; }

            public float Longitude { get; set; }
        }
    }
}