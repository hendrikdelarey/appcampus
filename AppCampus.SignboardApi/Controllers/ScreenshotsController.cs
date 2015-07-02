using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.SignboardApi.Models.InputModels;
using System.Reflection;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/signboards/{macAddress}/screenshots")]
    public class ScreenshotsController : ApiController
    {
        private IDeviceRepository DeviceRepository;
        private IScreenshotComponent ScreensaverComponent;
        private ILoggingComponent LoggingComponent;

        public ScreenshotsController(IDeviceRepository deviceRepository, IScreenshotComponent screensaverComponent, ILoggingComponent loggingComponent)
        {
            DeviceRepository = deviceRepository;
            ScreensaverComponent = screensaverComponent;
            LoggingComponent = loggingComponent;
        }

        [Route()]
        public IHttpActionResult Post(string macAddress, ScreenshotInputModel model)
        {
            if (model == null)
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), "model is null");
                return BadRequest();
            }

            if (!MacAddress.IsValid(macAddress))
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), "invalid macAddress");
                return BadRequest();
            }

            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));

            if (device == null)
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), "device does not exist (" + macAddress + ")");
                return BadRequest();
            }

            LoggingComponent.LogInfo(MethodBase.GetCurrentMethod(), "Screenshot taken and stored in database. ScreenshotId = '" + model.ScreenshotId + "'");
            ScreensaverComponent.StoreScreenshot(Screenshot.From(model.ScreenshotId, model.Base64ImageString, device.Id));

            return Ok();
        }
    }
}