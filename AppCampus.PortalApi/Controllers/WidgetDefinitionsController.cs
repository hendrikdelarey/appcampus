using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
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
    /// Widget definitions define the data structure and values behind widget instances.
    /// </summary>
    [RoutePrefix("api/v1/widgetDefinitions")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class WidgetDefinitionsController : ApiController
    {
        private IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public WidgetDefinitionsController(IWidgetDefinitionRepository widgetDefinitionRepository)
        {
            WidgetDefinitionRepository = widgetDefinitionRepository;
        }

        /// <summary>
        /// Retrieves a widget definition.
        /// </summary>
        /// <param name="widgetDefinitionId">The identifier of the widget definition.</param>
        /// <returns>A widget definition model.</returns>
        [Route("{widgetDefinitionId:Guid}", Name = "GetWidgetDefinition")]
        [ResponseType(typeof(WidgetDefinitionModel))]
        public IHttpActionResult Get(Guid widgetDefinitionId)
        {
            var widgetDefinition = WidgetDefinitionRepository.Find(widgetDefinitionId);

            if (widgetDefinition == null)
            {
                return NotFound();
            }

            return Ok(WidgetDefinitionModel.From(widgetDefinition));
        }

        /// <summary>
        /// Lists all the widget definitions.
        /// </summary>
        /// <returns>A list of widget definition models.</returns>
        [Route]
        [ResponseType(typeof(WidgetDefinitionModel))]
        public IHttpActionResult Get()
        {
            var widgetDefinitions = WidgetDefinitionRepository.GetAll();

            var responseModel = widgetDefinitions.Select(x => WidgetDefinitionModel.From(x));

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates a widget definition.
        /// </summary>
        /// <param name="model">The widget definition input model.</param>
        /// <returns>The created widget definition model.</returns>
        [Route]
        [OverrideAuthorization]
        [AuthoriseRoles(RoleClassification.SuperAdministrator)]
        [ResponseType(typeof(WidgetDefinitionModel))]
        public IHttpActionResult Post(WidgetDefinitionInputModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Invalid name for Widget Definition.");
            }

            WidgetDefinition widgetDefinition = new WidgetDefinition(model.Name);

            WidgetDefinitionRepository.Add(widgetDefinition);

            return Created(new Uri(Url.Link("GetWidgetDefinition", new { widgetDefinitionId = widgetDefinition.Id })), WidgetDefinitionModel.From(widgetDefinition));
        }
    }
}