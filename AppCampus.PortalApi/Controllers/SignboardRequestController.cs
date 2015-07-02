using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Enums;
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
    /// Requests sent to Signboards
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:companyId}/signboards/{signboardId:Guid}/requests")]
    [AuthoriseCompany]
    public class SignboardRequestController : ApiController
    {
        private ISignboardRepository SignboardRepository { get; set; }
        private ISoftwareRepository SoftwareRepository { get; set; }

        public SignboardRequestController(ISignboardRepository signboardRepository, ISoftwareRepository softwareRepository)
        {
            SignboardRepository = signboardRepository;
            SoftwareRepository = softwareRepository;
        }

        /// <summary>
        /// Retrieves a request
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <returns>List of Signboard Request Models.</returns>
        [Route("", Name = "GetSignboardRequests")]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        [ResponseType(typeof(List<SignboardRequestModel>))]
        public IHttpActionResult Get(Guid companyId, Guid signboardId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null || signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var responseModel = signboard.Requests.Select(x => SignboardRequestModel.From(x)).ToList();

            return Ok(responseModel);
        }

        /// <summary>
        /// Retrieve all the requests for the signboard
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <param name="requestId">The identifier of the signboards request.</param>
        /// <returns>Signboard Request Model.</returns>
        [Route("{requestId:Guid}", Name = "GetSignboardRequest")]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        [ResponseType(typeof(List<SignboardRequestModel>))]
        public IHttpActionResult Get(Guid companyId, Guid signboardId, Guid requestId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null || signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var request = signboard.Requests.FirstOrDefault(x => x.Id == requestId);

            if (request == null) 
            {
                return NotFound();
            }

            var responseModel = SignboardRequestModel.From(request);
            return Ok(responseModel);
        }

        /// <summary>
        /// Cancels the request.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the company's signboard.</param>
        /// <param name="requestId">The identifier of the signboards request.</param>
        /// <returns>Signboard Request Model.</returns>
        [Route("{requestId:Guid}")]
        [AuthoriseRoles(RoleClassification.GeneralUser)]
        [ResponseType(typeof(List<SignboardRequestModel>))]
        public IHttpActionResult Delete(Guid companyId, Guid signboardId, Guid requestId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null || signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var request = signboard.Requests.FirstOrDefault(x => x.Id == requestId);

            if (request == null)
            {
                return NotFound();
            }

            request.Cancel();

            var responseModel = SignboardRequestModel.From(request);
            return Ok(responseModel);
        }

        /// <summary>
        /// Creates a request from a device.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <param name="model">The signboard request input model.</param>
        /// <returns>List of Signboard Request Models.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.DeviceManager)]
        [ResponseType(typeof(List<SignboardRequestModel>))]
        public IHttpActionResult Post(Guid companyId, Guid signboardId, SignboardRequestInputModel model)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null || signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            if (model.RequestType == RequestType.Undefined)
            {
                return BadRequest();
            }

            if (model.RequestType == RequestType.SoftwareUpdate) 
            {
                if(String.IsNullOrEmpty(model.Value))
                {
                    return BadRequest();
                }

                Guid softwareId;
                if(!Guid.TryParse(model.Value, out softwareId))
                {
                    return BadRequest();
                }

                var softwareVersion = SoftwareRepository.GetFileName(softwareId);
                if(softwareVersion == null)
                {
                    return BadRequest();
                }
            }

            if (model.RequestType == RequestType.TakeScreenshot || model.RequestType == RequestType.RetrieveLog) 
            {
                model.Value = Guid.NewGuid().ToString();
            }

            var request = AppCampus.Domain.Models.ValueObjects.Request.From(model.RequestType, model.Value, DateTime.UtcNow);
            
            if (request.RequestType == RequestType.ShowScreensaver)
            {
                signboard.ToggleScreensaver();
                request.Processed();
            }
            else if (request.RequestType == RequestType.FontFactorUpdate) 
            {
                float value;

                if (float.TryParse(request.Value, out value) && value > 0)
                {
                    signboard.SetFontFactor(value);
                }
                else 
                {
                    return BadRequest("Invalid float for font factor");
                }
            }
            else
            {
                signboard.AddRequest(request);
            }

            SignboardRepository.Update(signboard);
            var responseModel = SignboardRequestModel.From(request); 
            
            return Created(new Uri(Url.Link("GetSignboardRequests", new { companyId = signboard.CompanyId, signboardId = signboard.Id })), responseModel);
        }
    }
}