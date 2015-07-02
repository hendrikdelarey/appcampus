using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Models.InputModels;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;

namespace AppCampus.Signboard.Components.SoftwareUpdate
{
    public class DownloadManager
    {
        private string macAddress;
        private Guid requestId;

        private string targetFolder;

        private static DownloadManager instance = null;

        private bool busyUpdating = false;

        public static DownloadManager GetInstance(string macAddress, Guid requestId)
        {
            if (instance == null)
            {
                instance = new DownloadManager(macAddress, requestId);
            }
            else
            {
                instance.requestId = requestId;
                instance.macAddress = macAddress;
            }
            return instance;
        }

        private DownloadManager(string macAddress, Guid requestId)
        {
            this.macAddress = macAddress;
            this.requestId = requestId;
        }

        public void DownloadFile(Uri sourceUrl, string targetFolder)
        {
            if (busyUpdating) 
            {
                Logger.Instance.Write("DownloadFile", LogLevel.Medium, "Download already in progress");
                return;
            }

            busyUpdating = true;

            DateTime now = DateTime.Now;
            string dateTimeStr = now.Date.Day.ToString() + now.Month.ToString() + now.Second.ToString() + now.Millisecond.ToString();
            string filename = "Signboard-" + dateTimeStr + ".msi";
            this.targetFolder = targetFolder + "\\" + filename;

            WebClient downloader = new WebClient();
            // fake as if you are a browser making the request.
            downloader.Headers.Add("User-Agent", "Mozilla/4.0 (compatible; MSIE 8.0)");
            downloader.DownloadFileCompleted += DownloadFileCompleted(filename);
            downloader.DownloadFileAsync(sourceUrl, this.targetFolder);
            // wait for the current thread to complete, since the async action will be on a new thread.
            while (downloader.IsBusy) { }
        }

        public AsyncCompletedEventHandler DownloadFileCompleted(string filename)
        {
            Action<object, AsyncCompletedEventArgs> action = (sender, e) =>
            {
                if (e.Error != null)
                {
                    Logger.Instance.Write("GetLatestSoftwareVersion", LogLevel.Medium, "Download of file failed.");
                    RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, false));
                    busyUpdating = false;
                    return;
                }
                Logger.Instance.Write("GetLatestSoftwareVersion", LogLevel.Low, "Download Competed");
                UpdateSoftwareFinalise(filename);
            };
            return new AsyncCompletedEventHandler(action);
        }

        private void UpdateSoftwareFinalise(string filename)
        {
            // display completion status.
            Logger.Instance.Write("GetLatestSoftwareVersion", LogLevel.Low, "Successful request sent back");
            RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, true));

            UpdateSoftware(filename);

            busyUpdating = false;
        }

        private void UpdateSoftware(string filename)
        {
            string exeFilePath = Assembly.GetExecutingAssembly().Location;
            string applicationFolder = Path.GetDirectoryName(exeFilePath);

            // stop kiosk mode
            Logger.Instance.Write("UpdateSoftware", LogLevel.Low, "Attempting to remove kiosk mode");
            Configuration.Configuration.Instance.RemoveStartupKioskModeApplicationPath();

            // run installer
            Logger.Instance.Write("UpdateSoftware", LogLevel.Low, "Starting Jarvis");
            string fullfilename = String.Format(this.targetFolder);

            try
            {
                Process.Start(String.Format("{0}\\jarvis.bat", applicationFolder), String.Format("\"{0}\"", this.targetFolder));
            }
            catch (Exception e) 
            {
                Logger.Instance.Write("UpdateSoftware", LogLevel.Critical, "Unable to run jarvis: " + e.Message.ToString());
            }
        }
    }
}