using AppCampus.Domain.Interfaces.Repositories;
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
    /// Represents the parameters for a widget. This resource must be used to retrieve the values for an instance of a widget definition.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/slideshows/{slideshowId:Guid}/slides/{slideId:Guid}/widgets/{widgetId:Guid}/parameters")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class ParametersController : ApiController
    {
        private ISlideshowRepository SlideshowRepository { get; set; }

        private IWidgetDefinitionRepository WidgetDefinitionRepository { get; set; }

        public ParametersController(ISlideshowRepository slideshowRepository, IWidgetDefinitionRepository widgetDefinitionRepository)
        {
            SlideshowRepository = slideshowRepository;
            WidgetDefinitionRepository = widgetDefinitionRepository;
        }

        /// <summary>
        /// Retrieves a parameter for a widget.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <param name="widgetId">The identifier of the slide's widget.</param>
        /// <param name="parameterName">The name of the widget's parameter</param>
        /// <returns>The parameter model</returns>
        [Route("{parameterName}", Name = "GetParameter")]
        [ResponseType(typeof(ParameterModel))]
        public IHttpActionResult Get(Guid companyId, Guid slideshowId, Guid slideId, Guid widgetId, string parameterName)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var slide = slideshow.Slides.Where(s => s.Id == slideId).FirstOrDefault();

            if (slide == null)
            {
                return NotFound();
            }

            var widget = slide.Widgets.Where(w => w.Id == widgetId).First();

            if (widget == null)
            {
                return NotFound();
            }

            var widgetDefinition = WidgetDefinitionRepository.Find(widget.WidgetDefinitionId);

            if (widgetDefinition == null)
            {
                return NotFound();
            }

            var parameterDefinition = widgetDefinition.ParameterDefinitions.SingleOrDefault(x => x.Name.Equals(parameterName));

            if (parameterDefinition == null)
            {
                return NotFound();
            }

            var parameter = widget.Parameters.SingleOrDefault(x => x.ParameterDefinitionId.Equals(parameterDefinition.Id));
            if (parameter == null)
            {
                return NotFound();
            }
            return Ok(ParameterModel.From(parameter));
        }

        /// <summary>
        /// Lists the parameters for a widget.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <param name="widgetId">The identifier of the slide's widget.</param>
        /// <returns>A list of parameter models</returns>
        [Route]
        [ResponseType(typeof(ParameterModel))]
        public IHttpActionResult GetParametersByWidgetId(Guid companyId, Guid slideshowId, Guid slideId, Guid widgetId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var slide = slideshow.Slides.Where(s => s.Id == slideId).FirstOrDefault();

            if (slide == null)
            {
                return NotFound();
            }

            var widget = slide.Widgets.Where(w => w.Id == widgetId).FirstOrDefault();

            if (widget == null)
            {
                return NotFound();
            }

            var model = widget.Parameters.Select(param => ParameterModel.From(param));

            return Ok(model);
        }

        /// <summary>
        /// Creates a parameter for a widget.
        /// If a parameter for the definition already exists it will replace it.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <param name="widgetId">The identifier of the slide's widget.</param>
        /// <param name="model">The parameter input model.</param>
        /// <returns>The created parameter model.</returns>
        [Route()]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(ParameterModel))]
        public IHttpActionResult Put(Guid companyId, Guid slideshowId, Guid slideId, Guid widgetId, ParameterInputModel model)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }
            var slide = slideshow.Slides.Where(s => s.Id == slideId).First();

            if (slide == null)
            {
                return NotFound();
            }

            var widget = slide.Widgets.Where(w => w.Id == widgetId).FirstOrDefault();
            if (widget == null)
            {
                return NotFound();
            }

            var widgetDefinition = WidgetDefinitionRepository.Find(widget.WidgetDefinitionId);
            if (widgetDefinition == null)
            {
                return BadRequest();
            }

            var parameterDefinition = widgetDefinition.ParameterDefinitions.SingleOrDefault(x => x.Name.Equals(model.ParameterName));
            if (parameterDefinition == null)
            {
                return BadRequest();
            }

            var parameter = Parameter.From(parameterDefinition.Id, model.Value);
            widget.AssignParameter(parameter);

            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetParameter", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id, slideId = slideId, widgetId = widget.Id, parameterName = model.ParameterName })), ParameterModel.From(parameter));
        }
    }
}