using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Signboards represent devices which are to be used to display information (slideshows).
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:companyId}/signboards")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class SignboardsController : ApiController
    {
        private ISignboardRepository SignboardRepository { get; set; }

        private IDeviceRepository DeviceRepository { get; set; }


        public SignboardsController(ISignboardRepository signboardRepository, IDeviceRepository deviceRepository)
        {
            SignboardRepository = signboardRepository;
            DeviceRepository = deviceRepository;
        }

        /// <summary>
        /// Retrieves a signboard.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <returns>The signboard model.</returns>
        [Route("{signboardId:Guid}", Name = "GetSignboard")]
        [ResponseType(typeof(SignboardModel))]
        public IHttpActionResult Get(Guid companyId, Guid signboardId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null || signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var device = DeviceRepository.Find(signboard.DeviceId);

            return Ok(SignboardModel.From(signboard, device.MacAddress));
        }

        /// <summary>
        /// Lists all of a company's signboards.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <returns>A list of signboard models.</returns>
        [Route]
        [ResponseType(typeof(ParameterModel))]
        public IHttpActionResult GetByCompanyId(Guid companyId)
        {
            var signboards = SignboardRepository.Get(companyId);

            var model = signboards.Select(x =>
                SignboardModel.From(
                    x,
                    DeviceRepository.Find(x.DeviceId).MacAddress
                ));

            return Ok(model);
        }

        /// <summary>
        /// Creates a signboard from a device.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The signboard input model.</param>
        /// <returns>The created signboard model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.DeviceManager)]
        [ResponseType(typeof(ParameterModel))]
        public IHttpActionResult Post(Guid companyId, SignboardInputModel model)
        {
            Signboard signboard;

            var device = DeviceRepository.GetByMacAddress(MacAddress.From(model.MacAddress));

            if (device == null || device.CompanyId != companyId)
            {
                return BadRequest("MAC address is not registered.");
            }
            else if (device.State == DeviceState.Approved)
            {
                return BadRequest("MAC address used by another signboard");
            }

            signboard = new Signboard(model.Name, companyId, device.Id, "0.0.0", 1);

            SignboardRepository.Add(signboard);

            device.Approve();

            DeviceRepository.Update(device);

            var responseModel = SignboardModel.From(signboard, device.MacAddress);

            return Created(new Uri(Url.Link("GetSignboard", new { signboardId = signboard.Id, companyId = signboard.CompanyId })), responseModel);
        }
    }
}