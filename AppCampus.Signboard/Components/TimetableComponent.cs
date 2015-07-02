using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Models;
using RestSharp;

namespace AppCampus.Signboard.Components
{
    public class TimetableComponent : GatewayComponent
    {
        public TimetableComponent() :
            base()
        {
        }

        public TimetableModel GetTimetable(string operatorName, string stopIdentifier, int numResults)
        {
            var request = new RestRequest("v1/timetables", Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddQueryParameter("OperatorName", operatorName);
            request.AddQueryParameter("StopIdentifier", stopIdentifier);
            request.AddQueryParameter("NumResults", numResults.ToString());


            var response = Client.Execute<TimetableModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                if (response.Data == null || response.Data.Operator == null || response.Data.StopName == null || response.Data.TimetableEntry == null) 
                {
                    return null;
                }

                return response.Data;
            }
            else
            {
                Logger.Instance.Write("GetTimetable", LogLevel.Medium, "Call failed with status code " + response.StatusCode);
                return null;
            }
        }
    }
}