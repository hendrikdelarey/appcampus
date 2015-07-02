using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.SignboardApi.Models.InputModels;
using AppCampus.SignboardApi.Models.ResponseModels;
using System.Linq;
using System.Reflection;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/signboards/{macAddress}/requests")]
    public class RequestsController : ApiController
    {
        public ISignboardRepository SignboardRepository;

        public IDeviceRepository DeviceRepository { get; set; }

        public ILoggingComponent LoggingComponent { get; set; }

        public RequestsController(ISignboardRepository signboardRepository, IDeviceRepository deviceRepository, ILoggingComponent loggingComponent)
        {
            SignboardRepository = signboardRepository;
            DeviceRepository = deviceRepository;
            LoggingComponent = loggingComponent;
        }

        [Route()]
        public IHttpActionResult PostRequest(string macAddress, RequestInputModel model)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));
            if (device == null)
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "Device with macAddress " + macAddress + " could not be found.");
                return NotFound();
            }

            var deviceId = device.Id;
            var signboard = SignboardRepository.GetByDevice(deviceId);

            if (signboard == null)
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "Signboard with macAddress " + macAddress + " could not be found.");
                return BadRequest();
            }

            Request request = signboard.Requests.FirstOrDefault(x => x.Id == model.RequestId);

            if (request == null)
            {
                LoggingComponent.LogError(MethodBase.GetCurrentMethod(), "Request can not be found.");
                return BadRequest();
            }

            if (model.Success)
            {
                request.Processed();
            }
            else
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "Request failed.");
                request.Failed();
            }

            SignboardRepository.Update(signboard);

            return Ok(RequestResponseModel.From(request));
        }
    }
}