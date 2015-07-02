using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Extensions;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.ActionModels;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// A slideshow contains a range of slides that will be displayed on a signboard for a given period of time.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/slideshows")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class SlideshowsController : CustomApiController
    {
        private ISlideshowRepository SlideshowRepository { get; set; }

        private ISignboardRepository SignboardRepository { get; set; }

        public SlideshowsController(ISlideshowRepository slideshowRepository, ISignboardRepository signboardRepository)
        {
            SlideshowRepository = slideshowRepository;
            SignboardRepository = signboardRepository;
        }

        /// <summary>
        /// Retrieves a slideshow.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <returns>The slideshow model.</returns>
        [Route("{slideshowId:Guid}", Name = "GetSlideshow")]
        [ResponseType(typeof(SlideshowModel))]
        public IHttpActionResult Get(Guid companyId, Guid slideshowId)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            return Ok(SlideshowModel.From(slideshow));
        }

        /// <summary>
        /// Lists all of a company's slideshows.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <returns>A list of slideshow models.</returns>
        [Route]
        [ResponseType(typeof(SlideshowModel))]
        public IHttpActionResult GetByCompanyId(Guid companyId)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            var signboards = SlideshowRepository.GetByCompany(companyId);

            var model = signboards.Select(slideshow => SlideshowModel.From(slideshow));

            return Ok(model);
        }

        /// <summary>
        /// Deletes a slideshow.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        [Route("{slideshowId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        public IHttpActionResult DeleteSlideshow(Guid companyId, Guid slideshowId)
        {
            var slideshow = SlideshowRepository.Find(slideshowId);

            if (slideshow == null || slideshow.CompanyId != companyId)
            {
                return NotFound();
            }

            if (SignboardRepository.HasScheduledSlideshow(slideshowId))
            {
                return BadRequest();
            }

            slideshow.DeleteSlideshow();
            SlideshowRepository.Update(slideshow);

            return NoContent();
        }

        /// <summary>
        /// Creates a slideshow.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The slideshow input model.</param>
        /// <returns>The created slideshow model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(SlideshowModel))]
        public IHttpActionResult Post(Guid companyId, SlideshowInputModel model)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Invalid name for Slideshow.");
            }

            if (SlideshowRepository.GetByCompany(companyId).Any(s => s.Name.Equals(model.Name)))
            {
                return BadRequest("A slideshow with name '" + model.Name + "' already exists.");
            }

            Slideshow slideshow = new Slideshow(model.Name, companyId);

            SlideshowRepository.Add(slideshow);

            return Created(new Uri(Url.Link("GetSlideshow", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id })), SlideshowModel.From(slideshow));
        }

        /// <summary>
        /// Creates a slideshow.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="slideshowId">The identifier of the slideshow.</param>
        /// <param name="model">The slideshow input model.</param>
        /// <returns>The created slideshow model.</returns>
        [Route("{slideshowId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(SlideshowModel))]
        public IHttpActionResult Put(Guid companyId, Guid slideshowId, SlideshowInputModel model)
        {
            if (companyId == null)
            {
                return NotFound();
            }

            if (String.IsNullOrWhiteSpace(model.Name))
            {
                return BadRequest("Invalid name for Slideshow.");
            }

            if (SlideshowRepository.GetByCompany(companyId).Any(s => s.Name.Equals(model.Name)))
            {
                return BadRequest("A slideshow with name '" + model.Name + "' already exists.");
            }

            Slideshow slideshow = SlideshowRepository.Find(slideshowId);
            if (slideshowId == null)
            {
                return NotFound();
            }

            slideshow.UpdateName(model.Name);

            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetSlideshow", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id })), SlideshowModel.From(slideshow));
        }

        /// <summary>
        /// Move a slide in a slideshow
        /// </summary>
        /// <param name="model">The slideshow input model.</param>
        /// <param name="slideshowId">The identifier of the company's slideshow.</param>
        /// <returns>The created slideshow model.</returns>

        [HttpPatch]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [Route("{slideshowId:Guid}")]
        public IHttpActionResult MoveSlide(Guid slideshowId, SlideshowUpdateActionModel model)
        {
            if (slideshowId == null)
            {
                return NotFound();
            }

            var slideshow = SlideshowRepository.Find(slideshowId);

            var slideToMove = slideshow.Slides.FirstOrDefault(x => x.Id == model.SlideId);
            var referencedSlide = slideshow.Slides.FirstOrDefault(x => x.Id == model.ReferencedSlideId);

            if (slideToMove == null || referencedSlide == null)
            {
                return NotFound();
            }

            switch (model.Operation)
            {
                case (SlideshowUpdateActionModel.SlideshowOperation.MoveBefore):
                    slideshow.MoveAfter(slideToMove, referencedSlide);
                    break;

                case (SlideshowUpdateActionModel.SlideshowOperation.MoveAfter):
                    slideshow.MoveBefore(slideToMove, referencedSlide);
                    break;

                default:
                    return BadRequest();
            }

            SlideshowRepository.Update(slideshow);

            return Created(new Uri(Url.Link("GetSlideshow", new { companyId = slideshow.CompanyId, slideshowId = slideshow.Id })), SlideshowModel.From(slideshow));
        }
    }
}