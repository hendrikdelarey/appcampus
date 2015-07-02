using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.SignboardApi.Models.QueryModels;
using AppCampus.SignboardApi.Models.ResponseModels;
using System;
using System.Reflection;
using System.Threading;
using System.Web.Http;

namespace AppCampus.SignboardApi.Controllers
{
    [RoutePrefix("signboardapi/v1/state")]
    public class StateController : ApiController
    {
        public ISignboardRepository SignboardRepository { get; set; }

        public IDeviceRepository DeviceRepository { get; set; }

        public ICompanyRepository CompanyRepository { get; set; }

        public ILoggingComponent LoggingComponent { get; set; }

        public StateController(ISignboardRepository signboardRepository, IDeviceRepository deviceRepository, ICompanyRepository companyRepository, ILoggingComponent loggingComponent)
        {
            SignboardRepository = signboardRepository;
            DeviceRepository = deviceRepository;
            CompanyRepository = companyRepository;
            LoggingComponent = loggingComponent;
        }

        [HttpGet]
        [Route("{macAddress}", Name = "GetState")]
        public IHttpActionResult GetState(string macAddress)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));

            if (device == null)
            {
                return Ok(StateModel.FromNoState());
            }

            Signboard signboard = null;

            if(device.State == DeviceState.Approved)
            {
                signboard = SignboardRepository.GetByDevice(device.Id);

                if(signboard == null)
                {
                    LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "System error:  There is no signboard associated with this device.");
                    return BadRequest("System error:  There is no signboard associated with this device.");
                }
            }

            return Ok(StateModel.From(device, signboard));
        }

        [HttpPost]
        [Route("{macAddress}/pending")]
        public IHttpActionResult RequestApproval(string macAddress, ApprovalInputModel approvalRequest)
        {
            var company = CompanyRepository.Find(approvalRequest.CompanyId);

            if (company == null)
            {
                LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "The associated company could not be found.");
                return BadRequest("The associated company could not be found.");
            }

            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));

            Signboard signboard = null;

            if (device == null)
            {
                device = Device.CreateNew(company.Id, MacAddress.From(macAddress), approvalRequest.Comment);

                DeviceRepository.Add(device);
            }
            else if (device != null)
            {
                device.Comment = approvalRequest.Comment;
                signboard = SignboardRepository.GetByDevice(device.Id);

                if (signboard == null)
                {
                    if (device.State == DeviceState.Declined)
                    {
                        device.Pend();
                        DeviceRepository.Update(device);
                    }
                    else if (device.State == DeviceState.Blocked)
                    {
                        LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "A blocked device cannot request approval.");
                        return BadRequest("A blocked device cannot request approval.");
                    }
                }
                else
                {
                    LoggingComponent.LogWarning(MethodBase.GetCurrentMethod(), "This device has already been approved.");
                    return BadRequest("This device has already been approved.");
                }
            }

            return Created(new Uri(Url.Link("GetState", new { macAddress = device.MacAddress.ToString() })), StateModel.From(device, signboard));
        }
    }
}