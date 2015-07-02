using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.PortalApi.Extensions;
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
    /// Slides represent individual "pages" on a slideshow.  Slides belong to a parent slideshow and cannot exist without it.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/slideshows/{slideshowId:Guid}/slides")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class SlidesController : CustomApiController
    {
        private ISlideshowRepository SlideshowRepository { get; set; }

        public SlidesController(ISlideshowRepository slideshowRepository)
        {
            SlideshowRepository = slideshowRepository;
        }

        /// <summary>
        /// Retrieves a slide.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slideshow's slide.</param>
        /// <returns>The slide model.</returns>
        [Route("{slideId:Guid}", Name = "GetSlide")]
        [ResponseType(typeof(SlideModel))]
        public IHttpActionResult Get(Guid companyId, Guid slideshowId, Guid slideId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var slide = slideshow.Slides.FirstOrDefault(s => s.Id == slideId);

            if (slide == null)
            {
                return NotFound();
            }

            return Ok(SlideModel.From(slide));
        }

        /// <summary>
        /// Lists all slides belonging to a slideshow.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <returns>A list of slide models.</returns>
        [Route]
        [ResponseType(typeof(SlideModel))]
        public IHttpActionResult GetSlidesBySlideshowId(Guid companyId, Guid slideshowId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            var responseModel = slideshow.Slides.Select(slide => SlideModel.From(slide));

            return Ok(responseModel);
        }

        /// <summary>
        /// Creates a slide at the position before the referenced slide.
        /// </summary>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the referenced slide to insert before.</param>
        /// <param name="model">The slide input model.</param>
        /// <returns>The created slide model.</returns>
        [Route("{slideId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(SlideModel))]
        public IHttpActionResult PostSlideBefore(Guid slideshowId, Guid slideId, SlideInputModel model)
        {
            if (slideshowId == null || slideId == null)
            {
                return NotFound();
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            var referencedSlide = slideshow.Slides.FirstOrDefault(x => x.Id == slideId);

            if (slideshow == null || referencedSlide == null)
            {
                return NotFound();
            }

            var slide = new Slide(new Colour(model.BackgroundColourHexCode), Duration.From(model.DurationInSeconds), Transition.From(model.TransitionType), model.Name);

            if (model.IsActive)
            {
                slide.ActivateSlide();
            }
            else
            {
                slide.DeactivateSlide();
            }

            slideshow.InsertBefore(slide, referencedSlide);
            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetSlide", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id, slideId = slide.Id })), SlideModel.From(slide));
        }

        /// <summary>
        /// Updates a slide at the position before the referenced slide.
        /// </summary>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the referenced slide to insert before.</param>
        /// <param name="model">The slide input model.</param>
        /// <returns>The created slide model.</returns>
        [Route("{slideId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(SlideModel))]
        public IHttpActionResult PutSlide(Guid slideshowId, Guid slideId, SlideInputModel model)
        {
            if (slideshowId == null || slideId == null)
            {
                return NotFound();
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null)
            {
                return NotFound();
            }

            var slide = slideshow.Slides.FirstOrDefault(x => x.Id == slideId);

            if (slide == null)
            {
                return NotFound();
            }

            slide.SetDuration(Duration.From(model.DurationInSeconds));

            slide.SetBackgroundColour(new Colour(model.BackgroundColourHexCode));

            slide.SetName(model.Name);

            if (model.IsActive)
            {
                slide.ActivateSlide();
            }
            else
            {
                slide.DeactivateSlide();
            }

            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetSlide", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id, slideId = slide.Id })), SlideModel.From(slide));
        }

        /// <summary>
        /// Creates a slide.
        /// </summary>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="model">The slide input model.</param>
        /// <returns>The created slide model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(SlideModel))]
        public IHttpActionResult PostSlide(Guid slideshowId, SlideInputModel model)
        {
            if (slideshowId == null)
            {
                return NotFound();
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null)
            {
                return NotFound();
            }

            var slide = new Slide(new Colour(model.BackgroundColourHexCode), Duration.From(model.DurationInSeconds), Transition.From(model.TransitionType), model.Name);

            if (model.IsActive)
            {
                slide.ActivateSlide();
            }
            else
            {
                slide.DeactivateSlide();
            }

            slideshow.Insert(slide);
            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetSlide", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id, slideId = slide.Id })), SlideModel.From(slide));
        }

        /// <summary>
        /// Deletes a slide.
        /// </summary>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <param name="slideId">The identifier of the slide to delete.</param>
        [Route("{slideId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        public IHttpActionResult DeleteSlide(Guid slideshowId, Guid slideId)
        {
            if (slideshowId == null)
            {
                throw new ArgumentNullException("slideshowId", "SlideshowId cannot be null");
            }

            if (slideId == null)
            {
                throw new ArgumentNullException("slideshowId", "SlideshowId cannot be null");
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null)
            {
                throw new ArgumentOutOfRangeException("slideshowId", "SlideshowId does not exist");
            }

            if (slideshow.Slides.SingleOrDefault(s => s.Id == slideId) == null)
            {
                throw new ArgumentOutOfRangeException("slideId", "SlideId does not exist");
            }

            slideshow.Slides.SingleOrDefault(s => s.Id == slideId).DeleteSlide();

            SlideshowRepository.Update(slideshow);

            return NoContent();
        }
    }
}