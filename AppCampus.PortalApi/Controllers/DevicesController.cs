using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Devices represent the physical hardware devices that host the signboard software. Devices are identified by their MAC address.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:companyId}/devices")]
    [AuthoriseCompany]
    public class DevicesController : ApiController
    {
        private IDeviceRepository DeviceRepository { get; set; }

        public DevicesController(IDeviceRepository deviceRepository)
        {
            DeviceRepository = deviceRepository;
        }

        /// <summary>
        /// Retrieves a company's device.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="macAddress">The identifier (MAC address) of the device.</param>
        /// <returns>The device model.</returns>
        [Route("{macAddress:macAddress}", Name = "GetDevice")]
        [ResponseType(typeof(DeviceModel))]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        public IHttpActionResult GetByMacAddress(Guid companyId, string macAddress)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(macAddress));

            if (device == null || device.CompanyId != companyId)
            {
                return NotFound();
            }

            var response = DeviceModel.From(device);

            return Ok(response);
        }

        /// <summary>
        /// Lists all of a company's devices.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <returns>A list of device models.</returns>
        [Route]
        [ResponseType(typeof(DeviceModel))]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        public IHttpActionResult Get(Guid companyId)
        {
            var devices = DeviceRepository.Get(companyId);

            var response = devices.Select(x => DeviceModel.From(x));

            return Ok(response);
        }

        /// <summary>
        /// Lists all of a company's devices filtered by device state.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="state">The device state to filter the list by.</param>
        /// <returns>A list of device models.</returns>
        [Route("{state:deviceState}")]
        [ResponseType(typeof(DeviceModel))]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        public IHttpActionResult GetByState(Guid companyId, DeviceState state)
        {
            IReadOnlyCollection<Device> devices;

            if (state == DeviceState.Pending)
            {
                devices = DeviceRepository.GetPending(companyId);
            }
            else if (state == DeviceState.Approved)
            {
                devices = DeviceRepository.GetApproved(companyId);
            }
            else if (state == DeviceState.Declined)
            {
                devices = DeviceRepository.GetDeclined(companyId);
            }
            else if (state == DeviceState.Blocked)
            {
                devices = DeviceRepository.GetBlocked(companyId);
            }
            else
            {
                throw new NotImplementedException(String.Format("There is no implementation for DeviceState '{0}'", state));
            }

            var model = devices.Select(x => DeviceModel.From(x));

            return Ok(model);
        }

        /// <summary>
        /// Declines a device.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The device model.</param>
        /// <returns>The declined device.</returns>
        [Route("declined")]
        [AuthoriseRoles(RoleClassification.DeviceManager)]
        [ResponseType(typeof(DeviceModel))]
        public IHttpActionResult PostDeclined(Guid companyId, DeviceInputModel model)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(model.MacAddress));

            if (device == null || device.CompanyId != companyId)
            {
                return NotFound();
            }
            device.Comment = model.Comment;

            if (device.State == DeviceState.Approved)
            {
                return BadRequest("Cannot decline an already approved device.");
            }

            device.Decline();

            DeviceRepository.Update(device);

            return Created(new Uri(Url.Link("GetDevice", new { macAddress = device.MacAddress.ToString() })), DeviceModel.From(device));
        }

        /// <summary>
        /// Blocks a device. This prevents the device from further requests.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The device model.</param>
        /// <returns>The blocked device.</returns>
        [Route("blocked")]
        [AuthoriseRoles(RoleClassification.DeviceManager)]
        [ResponseType(typeof(DeviceModel))]
        public IHttpActionResult PostBlocked(Guid companyId, DeviceInputModel model)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(model.MacAddress));

            if (device == null || device.CompanyId != companyId)
            {
                return NotFound();
            }
            device.Comment = model.Comment;

            if (device.State == DeviceState.Approved)
            {
                return BadRequest("Cannot block an already approved device.");
            }

            device.Block();

            DeviceRepository.Update(device);

            return Created(new Uri(Url.Link("GetDevice", new { macAddress = device.MacAddress.ToString() })), DeviceModel.From(device));
        }

        /// <summary>
        /// Creates a device. The device will be in a pending state.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The device input model.</param>
        /// <returns>The created device model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.DeviceManager)]
        [ResponseType(typeof(DeviceModel))]
        public IHttpActionResult Post(Guid companyId, DeviceInputModel model)
        {
            var device = DeviceRepository.GetByMacAddress(MacAddress.From(model.MacAddress));

            if (device == null)
            {
                device = Device.CreateNew(companyId, MacAddress.From(model.MacAddress), model.Comment);

                DeviceRepository.Add(device);

                var response = DeviceModel.From(device);

                return Created(new Uri(Url.Link("GetDevice", new { macAddress = device.MacAddress.ToString() })), response);
            }
            else if (device != null)
            {
                device.Comment = model.Comment;
                if (device.State == DeviceState.Declined)
                {
                    return BadRequest("Device is declined");
                }
                else if (device.State == DeviceState.Blocked)
                {
                    return BadRequest("Device is blocked");
                }
            }
            return BadRequest("Internal error");
        }
    }
}