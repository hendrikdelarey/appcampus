using AppCampus.Domain.Interfaces.Components;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.PortalApi.Filters;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Stop names and codes that are available for TimetableWidget.
    /// </summary>
    [RoutePrefix("api/v1/widgetcontent/stops")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class StopsController : ApiController
    {
        private IDrumbleComponent DrumbleComponent;

        public StopsController(IDrumbleComponent DrumbleComponent)
        {
            DrumbleComponent = DrumbleComponent;
        }

        /// <summary>
        /// Returns a list of Stop Objects
        /// </summary>
        [Route]
        [ResponseType(typeof(List<Stop>))]
        public IHttpActionResult Get()
        {
            var response = DrumbleComponent.GetStops();

            return Ok(response);
        }
    }
}