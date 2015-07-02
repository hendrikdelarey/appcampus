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
    /// Parameter definitions define the parameter values required for a widget instance.  Parameter definitions belong to a widget definition.
    /// </summary>
    [RoutePrefix("api/v1/widgetDefinitions/{widgetDefinitionId:Guid}/parameterDefinitions")]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class ParameterDefinitionsController : ApiController
    {
        private IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public ParameterDefinitionsController(IWidgetDefinitionRepository widgetDefinitionRepository)
        {
            WidgetDefinitionRepository = widgetDefinitionRepository;
        }

        /// <summary>
        /// Retrieves a parameter definition.
        /// </summary>
        /// <param name="widgetDefinitionId">The identifier of the widget definition.</param>
        /// <param name="parameterName">The name of the parameter definition.</param>
        /// <returns>The parameter definition model.</returns>
        [Route("{parameterName}", Name = "GetParameterDefinition")]
        [ResponseType(typeof(ParameterDefinitionModel))]
        public IHttpActionResult Get(Guid widgetDefinitionId, string parameterName)
        {
            var widgetDefinition = WidgetDefinitionRepository.Find(widgetDefinitionId);

            if (widgetDefinition == null || String.IsNullOrWhiteSpace(parameterName))
            {
                return NotFound();
            }

            var parameterDefinition = widgetDefinition.ParameterDefinitions.FirstOrDefault(x => x.Name.Equals(parameterName));

            if (parameterDefinition == null)
            {
                return NotFound();
            }

            return Ok(ParameterDefinitionModel.From(parameterDefinition));
        }

        /// <summary>
        /// Lists all parameter definitions for a widget definition.
        /// </summary>
        /// <param name="widgetDefinitionId">The identifier of the widget definition.</param>
        /// <returns>A list of parameter definition models.</returns>
        [Route]
        [ResponseType(typeof(ParameterDefinitionModel))]
        public IHttpActionResult Get(Guid widgetDefinitionId)
        {
            var widgetDefinition = WidgetDefinitionRepository.Find(widgetDefinitionId);

            if (widgetDefinition == null)
            {
                return NotFound();
            }

            var responseModel = widgetDefinition.ParameterDefinitions.Select(parameterDefinition =>
                    ParameterDefinitionModel.From(parameterDefinition));

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates a parameter definition for a widget definition.
        /// </summary>
        /// <param name="widgetDefinitionId">The identifier of the widget definition.</param>
        /// <param name="model">The parameter definition input model.</param>
        /// <returns>The created parameter definition model.</returns>
        [Route]
        [OverrideAuthorization]
        [AuthoriseRoles(RoleClassification.SuperAdministrator)]
        [ResponseType(typeof(ParameterDefinitionModel))]
        public IHttpActionResult Post(Guid widgetDefinitionId, ParameterDefinitionInputModel model)
        {
            if (String.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Invalid name for Widget Definition.");
            }

            var widgetDefinition = WidgetDefinitionRepository.Find(widgetDefinitionId);

            if (widgetDefinition == null)
            {
                return BadRequest("Invalid WidgetDefinitionId");
            }

            ParameterDefinition parameterDefinition = new ParameterDefinition(model.Name, model.DefaultValue, model.ParameterType);
            widgetDefinition.AddParameterDefinition(parameterDefinition);

            WidgetDefinitionRepository.Update(widgetDefinition);

            return Created(new Uri(Url.Link("GetParameterDefinition", new { parameterName = model.Name })), ParameterDefinitionModel.From(parameterDefinition));
        }
    }
}