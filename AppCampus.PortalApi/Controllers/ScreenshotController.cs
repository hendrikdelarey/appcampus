using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Screenshot represents a Base64Image string of captured by the device of its screen.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:companyId}/devices/{macAddress:macAddress}/screenshots")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class ScreenshotController : ApiController
    {
        private IScreenshotComponent ScreenshotComponent { get; set; }

        public ScreenshotController (IScreenshotComponent screenshotComponent)
        {
            ScreenshotComponent = screenshotComponent;
        }

        /// <summary>
        /// Retrieves a screenshot of the device.
        /// </summary>
        /// <param name="screenshotId">The identifier of the screenshot.</param>
        /// <returns>The ScreenshotModel.</returns>
        [Route("{screenshotId:Guid}")]
        [ResponseType(typeof(ScreenshotModel))]
        public IHttpActionResult GetScreenshot(Guid screenshotId)
        {
            if (!ScreenshotComponent.HasScreenshot(screenshotId)) 
            {
                return NotFound();
            }

            return Ok(ScreenshotModel.From(ScreenshotComponent.Find(screenshotId)));
        }
    }
}