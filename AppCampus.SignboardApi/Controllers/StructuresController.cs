using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Repositories.SlideshowAggregate;
using AppCampus.SignboardApi.Models.QueryModels;
using AppCampus.SignboardApi.Models.ResponseModels;
using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/signboards/{macAddress}")]
    public class StructuresController : ApiController
    {
        public ISlideshowRepository SlideshowRepository { get; set; }

        public IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public IAnnouncementRepository AnnouncementRepository { get; set; }

        public IDiagnosticsComponent DiagnosticsComponent { get; set; }

        public ISignboardRepository SignboardRepository { get; set; }

        public IDeviceRepository DeviceRepository { get; set; }

        public ILoggingComponent LoggingComponent { get; set; }

        public StructuresController(IDeviceRepository deviceRepository,
            ISignboardRepository signboardRepository,
            ISlideshowRepository slideshowRepository,
            IWidgetDefinitionRepository widgetDefinitionRepository,
            IAnnouncementRepository announcementRepository,
            IDiagnosticsComponent diagnosticsComponent,
            ILoggingComponent loggingComponent)
        {
            DeviceRepository = deviceRepository;
            SignboardRepository = signboardRepository;
            SlideshowRepository = slideshowRepository;
            WidgetDefinitionRepository = widgetDefinitionRepository;
            AnnouncementRepository = announcementRepository;
            DiagnosticsComponent = diagnosticsComponent;
            LoggingComponent = loggingComponent;
        }

        private string GetWidgetDefinitionType(Widget widget)
        {
            return WidgetDefinitionRepository.Find(widget.WidgetDefinitionId).Name.ToString();
        }

        private string GetParameterDefinitionType(Widget widget, Parameter parameter)
        {
            return WidgetDefinitionRepository.Find(widget.WidgetDefinitionId).ParameterDefinitions.Where(p => p.Id == parameter.ParameterDefinitionId).First().Name.ToString();
        }

        [HttpPost]
        [Route()]
        public IHttpActionResult PostStructure(string macAddress, DiagnosticInputModel diagnosticsModel)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));

            if (device == null)
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), String.Format("Device with mac address {0} not found. Possible configuration error.", macAddress));
                return NotFound();
            }

            var deviceId = device.Id;
            var signboard = SignboardRepository.GetByDevice(deviceId);

            if (signboard == null)
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), String.Format("Signboard with mac address {0} not found. Possible configuration error.", macAddress));
                return BadRequest();
            }

            var diagnostics = GetSignboardDiagnostics(signboard.Id, diagnosticsModel);
            DiagnosticsComponent.Write(diagnostics);

            var slideshow = SlideshowRepository.GetActiveBySignboard(signboard.Id);

            if (slideshow == null)
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "There is no slideshow attached to signboard with mac address " + macAddress);
            }

            var announcements = AnnouncementRepository.GetActiveBySignboard(signboard.Id);
            var announcementResponseModel = announcements.Select(x => AnnouncementResponseModel.From(x)).ToList();

            var requests = signboard.Requests.Where(x => x.State != RequestState.Cancelled && x.State != RequestState.Processed && x.State != RequestState.Sent);
            var signboardRequests = requests.Select(x => RequestResponseModel.From(x)).ToList();

            foreach (Request request in requests)
            {
                request.Sent();
            }

            var returnModel = GetStructureResponseModel(slideshow, announcementResponseModel, signboardRequests, signboard.IsShowingScreensaver, signboard.FontFactor);

            return Ok(returnModel);
        }

        private StructureResponseModel GetStructureResponseModel(Slideshow slideshow, List<AnnouncementResponseModel> announcementResponseModel, List<RequestResponseModel> signboardRequests, bool isShowingScreensaver, float fontFactor)
        {
            var returnVal = new StructureResponseModel()
            {
                Slideshow = slideshow == null ? null :
                new SlideshowResponseModel()
                {
                    Slides = slideshow.Slides.Where(x => x.IsActive && !x.IsDeleted).Select(slide => new SlideResponseModel()
                    {
                        BackgroundColour = slide.BackgroundColour.ColourHex,
                        DurationInSeconds = slide.Duration.Seconds,
                        Widgets = slide.Widgets.Select(widget =>
                            new WidgetResponseModel()
                            {
                                Type = GetWidgetDefinitionType(widget),
                                // Assumption: Widgets are all fullscreen
                                Position = new WidgetPositionResponseModel()
                                {
                                    StartColumnWeight = 0.0f,
                                    StartRowWeight = 0.0f,
                                    EndColumnWeight = 1.0f,
                                    EndRowWeight = 1.0f
                                },
                                Parameters = widget.Parameters.Select(parameter => new ParameterResponseModel()
                                {
                                    Definition = GetParameterDefinitionType(widget, parameter),
                                    Value = (parameter.Value == null) ?
                                        WidgetDefinitionRepository.Find(widget.WidgetDefinitionId).ParameterDefinitions.Single(x => x.Id == parameter.ParameterDefinitionId).DefaultValue
                                        :
                                        parameter.Value
                                }).ToList()
                            }
                        ).ToList()
                    }).ToList()
                },
                Announcements = announcementResponseModel,
                PollingIntervalInSeconds = Int32.Parse(CloudConfigurationManager.GetSetting("SignboardPollingIntervalInSeconds")),
                Requests = signboardRequests,
                IsShowingScreensaver = isShowingScreensaver,
                FontFactor = fontFactor
            };

            if (returnVal == null)
            {
                return null;
            }

            if (returnVal.Slideshow != null)
            {
                // add default values if there are no values supplied
                foreach (SlideResponseModel slide in returnVal.Slideshow.Slides)
                {
                    foreach (WidgetResponseModel widget in slide.Widgets)
                    {
                        WidgetDefinition widgetDefinition = WidgetDefinitionRepository.FindByName(widget.Type);

                        foreach (ParameterDefinition parameterDefinition in widgetDefinition.ParameterDefinitions)
                        {
                            if (!widget.Parameters.Select(x => x.Definition).Contains(parameterDefinition.Name))
                            {
                                widget.Parameters.Add(new ParameterResponseModel()
                                {
                                    Definition = parameterDefinition.Name,
                                    Value = parameterDefinition.DefaultValue
                                });
                            }
                        }
                    }
                }
            }

            return returnVal;
        }

        private SignboardDiagnostics GetSignboardDiagnostics(Guid signboardId, DiagnosticInputModel diagnosticsModel)
        {
            var diagnostics = new SignboardDiagnostics(signboardId, diagnosticsModel.DiagnosticDate, SoftwareVersion.From(diagnosticsModel.SoftwareVersion));
            foreach (DiagnosticMetricInputModel metric in diagnosticsModel.Metrics)
            {
                diagnostics.AddMetricValue((DiagnosticMetricType)Enum.Parse(typeof(DiagnosticMetricType), metric.Name), metric.Value.ToString());
            }

            if (diagnosticsModel.SignboardHealth == null)
            {
                return diagnostics;
            }
            else
            {
                diagnostics.AddMetricValue(DiagnosticMetricType.SignboardState, diagnosticsModel.SignboardHealth.ScreenState);
                diagnostics.AddMetricValue(DiagnosticMetricType.IsShowingScreensaver, diagnosticsModel.SignboardHealth.IsShowingScreensaver.ToString());

                if (diagnosticsModel.SignboardHealth.SlideHealth == null || diagnosticsModel.SignboardHealth.SlideHealth.Count == 0)
                {
                    return diagnostics;
                }

                diagnostics.AddMetricValue(DiagnosticMetricType.SlideshowHealthMetric, diagnosticsModel.SignboardHealth.ToString());
            }

            return diagnostics;
        }
    }
}