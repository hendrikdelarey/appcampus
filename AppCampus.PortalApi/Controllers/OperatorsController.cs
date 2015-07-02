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
    /// Operator names that are available for TimetableWidget.
    /// </summary>
    [RoutePrefix("api/v1/widgetcontent/operators")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class OperatorsController : ApiController
    {
        private IDrumbleComponent DrumbleComponent;

        public OperatorsController(IDrumbleComponent DrumbleComponent)
        {
            DrumbleComponent = DrumbleComponent;
        }

        /// <summary>
        /// Returns a list of Operators
        /// </summary>
        [Route]
        [ResponseType(typeof(List<Operator>))]
        public IHttpActionResult Get()
        {
            var response = DrumbleComponent.GetOperators();

            return Ok(response);
        }
    }
}