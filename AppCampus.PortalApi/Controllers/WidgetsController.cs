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
    /// Widgets contain the all relevant information necessary to display information as defined in the widget definition.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/slideshows/{slideshowId:Guid}/slides/{slideId:Guid}/widgets")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class WidgetsController : ApiController
    {
        private ISlideshowRepository SlideshowRepository { get; set; }

        public WidgetsController(ISlideshowRepository slideshowRepository)
        {
            SlideshowRepository = slideshowRepository;
        }

        /// <summary>
        /// Retrieves a widget.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <param name="widgetId">The identifier of the slide's widget.</param>
        /// <returns>The widget model.</returns>
        [Route("{widgetId:Guid}", Name = "GetWidget")]
        [ResponseType(typeof(WidgetModel))]
        public IHttpActionResult Get(Guid companyId, Guid slideshowId, Guid slideId, Guid widgetId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var slides = slideshow.Slides.SingleOrDefault(s => s.Id == slideId);

            if (slides == null)
            {
                return NotFound();
            }

            Widget widget = slides.Widgets.SingleOrDefault(x => x.Id == widgetId);

            if (widget == null)
            {
                return NotFound();
            }

            return Ok(WidgetModel.From(widget));
        }

        /// <summary>
        /// Lists a slide's widgets.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <returns>A list of widget models.</returns>
        [Route]
        [ResponseType(typeof(WidgetModel))]
        public IHttpActionResult GetWidgetsBySlideId(Guid companyId, Guid slideshowId, Guid slideId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var slide = slideshow.Slides.SingleOrDefault(s => s.Id == slideId);

            if (slide == null)
            {
                return NotFound();
            }

            var responseModel = slide.Widgets.Select(widget => WidgetModel.From(widget));

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates a widget on a slide.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <param name="model">The widget input model.</param>
        /// <returns>The created widget model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(WidgetModel))]
        public IHttpActionResult Post(Guid companyId, Guid slideshowId, Guid slideId, WidgetInputModel model)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            Widget widget = new Widget(model.WidgetDefinitionId, WidgetLayout.From(1, 1));

            var slide = slideshow.Slides.Where(s => s.Id == slideId).FirstOrDefault();
            if (slide == null)
            {
                return NotFound();
            }

            slide.AddWidget(widget);
            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetWidget", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id, slideId = slideId, widgetId = widget.Id })), WidgetModel.From(widget));
        }
    }
}