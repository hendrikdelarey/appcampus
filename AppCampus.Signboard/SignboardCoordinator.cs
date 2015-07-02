using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Diagnostics;
using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Models.InputModels;
using AppCampus.Signboard.Models.QueryModels;
using System;

namespace AppCampus.Signboard
{
    public class SignboardCoordinator
    {
        private DiagnosticsComponent diagnosticsComponent { get; set; }

        private string macAddress { get; set; }

        private int pollingIntervalInSeconds;

        public delegate void OnShowScreensaver(bool isShowScreensaver);

        public delegate void OnToggleScreensaver(bool showScreensaver);

        public int PollingIntervalInSeconds
        {
            get
            {
                return pollingIntervalInSeconds;
            }
            set
            {
                if (value < 10)
                {
                    pollingIntervalInSeconds = 10;
                }
                else
                {
                    pollingIntervalInSeconds = value;
                }
            }
        }

        private SlideshowState SlideshowState { get; set; }

        private void handleRequest(Request request, Structure structure)
        {
            Logger.Instance.Write("handleRequest", LogLevel.Low, "Handling Request " + request.RequestType.ToString() + " with value '" + request.Value ?? request.Value + "'");

            if (request.RequestType == RequestType.SoftwareUpdate)
            {
                SoftwareUpdateComponent.GetInstance(macAddress, request.Id).GetLatestSoftwareVersion(request.Value);
            }
            else if (request.RequestType == RequestType.RestartDevice)
            {
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, true));
                HardwareComponent.Restart();
            }
            else if (request.RequestType == RequestType.TakeScreenshot)
            {
                TakeScreenshot(request);
            }
            else if (request.RequestType == RequestType.RetrieveLog)
            {
                RetrieveLog(request);
            }
        }

        private void UpdateFontFactor(Request request, Structure structure)
        {
            float value = -1;
            float.TryParse(request.Value, out value);

            if (value < 0)
            {
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, false));
                return;
            }

            structure.FontSizeFactor = value;
            RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, true));
        }

        private void RetrieveLog(Request request)
        {
            string filename = request.Value;

            if (String.IsNullOrWhiteSpace(filename))
            {
                Logger.Instance.Write("RetrieveLog", LogLevel.Medium, "Invalid filename to retrieve");
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, false));
            }

            LogRetriever.GetInstance().PostLog(macAddress, filename, request.Id);
        }

        private void TakeScreenshot(Request request)
        {
            Guid screenshotId;
            if (request.Value == null || !Guid.TryParse(request.Value, out screenshotId))
            {
                Logger.Instance.Write("TakeScreenshot", LogLevel.Medium, "Invalid value for request");
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, false));
                return;
            }

            if (ScreenshotComponent.GetInstance().CreateScreenshot())
            {
                ScreenshotComponent.GetInstance().SendScreenshot(screenshotId, macAddress, request.Id);
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, true));
            }
            else
            {
                RequestComponent.GetInstance().FinishRequest(macAddress, Models.InputModels.RequestInputModel.From(request.Id, false));
            }
        }

        public SignboardCoordinator(string macAddress)
        {
            PollingIntervalInSeconds = 10;
            diagnosticsComponent = new DiagnosticsComponent(10);
            this.macAddress = macAddress;
            SlideshowState = new SlideshowState();
        }

        public SlideshowState UpdateStructure(SignboardHealth signboardHealth)
        {
            var diagnosticModel = DiagnosticModel.From(diagnosticsComponent, signboardHealth);

            var structure = SlideshowStructureComponent.GetInstance().GetSlideshowStructure(macAddress, diagnosticModel);

            if (structure == null)
            {
                return null;
            }

            bool hardReload = false;
            if (structure.Requests != null)
            {
                foreach (Request request in structure.Requests)
                {
                    handleRequest(request, structure);
                }
            }

            if (structure != null)
            {
                SlideshowState.Update(structure.Copy());
            }

            SlideshowState.HardReload = hardReload;

            SlideshowState.IsShowScreensaver = structure.IsShowScreensaver;

            SlideshowState.FontFactor = structure.FontSizeFactor;

            return SlideshowState;
        }
    }
}