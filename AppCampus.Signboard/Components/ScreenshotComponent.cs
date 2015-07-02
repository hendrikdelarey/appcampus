using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.ScreenCapture;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Models.InputModels;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace AppCampus.Signboard.Components
{
    public class ScreenshotComponent : GatewayComponent
    {
        private string base64Image = null;

        private static ScreenshotComponent instance = null;

        private ScreenshotComponent() :
            base() 
        {}

        public static ScreenshotComponent GetInstance()
        {
            if (instance == null)
            {
                instance = new ScreenshotComponent();
            }
                
            return instance;
        }

        public void SendScreenshot(Guid screenshotId, string macAddress, Guid requestId)
        {
            if (base64Image == null) 
            {
                Logger.Instance.Write("SendScreenshot", LogLevel.Medium, "No screenshot taken.");
                return;
            }

            var request = new RestRequest("v1/signboards/{macAddress}/screenshots", Method.POST);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddUrlSegment("macAddress", macAddress);
            request.AddBody(ScreenshotModel.From(screenshotId, base64Image));

            var response = Client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Logger.Instance.Write("SendScreenshot", LogLevel.Medium, "Screenshot request successfull");
                RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, true));
                return;
            }
            else
            {
                Logger.Instance.Write("SendScreenshot", LogLevel.Medium, "Screenshot request failed. " + ((response != null && response.ErrorMessage != null ) ? response.ErrorMessage.ToString() : " Response object is null"));
                RequestComponent.GetInstance().FinishRequest(macAddress, RequestInputModel.From(requestId, false));
                return;
            }
        }

        public bool CreateScreenshot()
        {
            try
            {
                var bitmapImage = CaptureScreen.CaptureDesktopWithCursor();
                var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetEntryAssembly().GetName().Name);
                directory = Path.Combine(directory, "screenshot.jpeg");
                bitmapImage.Save(directory, ImageFormat.Jpeg);

                ImageToBase64();
            }
            catch (ExternalException) 
            {
                Logger.Instance.Write("CreateScreenshot", LogLevel.Medium, "Failed to create screenshot and store it locally");
                return false;
            }

            return true;
        }

        private void ImageToBase64()
        {
            var bitmapImage = CaptureScreen.CaptureDesktopWithCursor();
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetEntryAssembly().GetName().Name);
            directory = Path.Combine(directory, "screenshot.jpeg");

            System.Drawing.Image img = System.Drawing.Image.FromFile(directory);
            byte[] arr;
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }

            base64Image = Convert.ToBase64String(arr);
        }
    }
}
