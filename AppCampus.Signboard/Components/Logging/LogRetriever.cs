using AppCampus.Signboard.Models.InputModels;
using AppCampus.Signboard.Models.ResponseModels;
using RestSharp;
using System;
using System.IO;
using System.Reflection;

namespace AppCampus.Signboard.Components.Logging
{
    public class LogRetriever : GatewayComponent
    {
        private static LogRetriever instance { get; set; }

        private readonly string loggingDirectory;

        private LogRetriever()
        {
            loggingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetEntryAssembly().GetName().Name);
            loggingDirectory = Path.Combine(loggingDirectory, "logs");
        }

        public static LogRetriever GetInstance()
        {
            if (instance == null)
            {
                return new LogRetriever();
            }

            return instance;
        }

        private string GetFilePath(string fileName)
        {
            // make sure fileName is in format YYYY-MM-DD
            return Path.Combine(loggingDirectory, String.Format("{0}.log", fileName));
        }

        private void PostLogFile(string macAddress, Guid requestId, Guid logFileId, string fileName)
        {
            string path = GetFilePath(fileName);

            if (!File.Exists(path))
            {
                Logger.Instance.Write("PostLogFile", LogLevel.Medium, "PostLog failed as the log file does not exist.");
                RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, false));
                return;
            }

            var request = new RestRequest("v1/signboards/{macAddress}/logFiles/{logFileId}/file", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("macAddress", macAddress);
            request.AddUrlSegment("logFileId", logFileId.ToString());
            request.AddBody(DeviceLogModel.From(fileName));
            request.AddFile("file", File.ReadAllBytes(path), Path.GetFileName(path), "multipart/form-data");

            Client.ExecuteAsync<DeviceLogResponseModel>(request, (response) =>
            {
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Logger.Instance.Write("PostLogFile", LogLevel.Low, "Uploaded  log file successfully.");
                    RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, true));
                    return;
                }
                else
                {
                    Logger.Instance.Write("PostLog", LogLevel.Medium, "PostLog request failed. ");
                    RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, false));
                    return;
                }
            });
        }

        public void PostLog(string macAddress, string fileName, Guid requestId)
        {
            var request = new RestRequest("v1/signboards/{macAddress}/logFiles", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("macAddress", macAddress);
            request.AddBody(DeviceLogModel.From(fileName));

            var response = Client.Execute<DeviceLogResponseModel>(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Logger.Instance.Write("PostLog", LogLevel.Low, "PostLog successful.");
                PostLogFile(macAddress, requestId, response.Data.LogFileId, fileName);
            }
            else
            {
                Logger.Instance.Write("PostLog", LogLevel.Medium, "PostLog request failed. " + response.ErrorMessage.ToString());
                RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, false));
                return;
            }
        }
    }
}