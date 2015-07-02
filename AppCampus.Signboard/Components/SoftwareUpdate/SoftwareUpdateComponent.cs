using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.SoftwareUpdate;
using AppCampus.Signboard.Models.InputModels;
using AppCampus.Signboard.Models.ResponseModels;
using RestSharp;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;

namespace AppCampus.Signboard.Components
{
    public class SoftwareUpdateComponent : GatewayComponent
    {
        private string macAddress;
        private Guid requestId;

        private SoftwareUpdateComponent(string macAddress, Guid requestId)
            : base()
        {
            this.macAddress = macAddress;
            this.requestId = requestId;
        }

        public void GetLatestSoftwareVersion( string softwareId)
        {
            string downloadPath = "/v1/signboardsoftware/" + softwareId + "/file";
            Uri downloadUri = new Uri(ConfigurationManager.AppSettings["SignboardApiUri"] + downloadPath);

            Logger.Instance.Write("GetLatestSoftwareVersion", LogLevel.Low, "Getting latest software");

            string storagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetEntryAssembly().GetName().Name).ToString();

            DownloadManager downloadManager = DownloadManager.GetInstance(macAddress, requestId);
            downloadManager.DownloadFile(downloadUri, storagePath);
        }

        public static SoftwareUpdateComponent GetInstance(string macAddress, Guid requestId) 
        {
            return new SoftwareUpdateComponent(macAddress, requestId);
        }
    }
}