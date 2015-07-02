using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Extensions;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// A scheduled slideshow is a slideshow that has been assigned to display on a signboard.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/signboards/{signboardId:Guid}/scheduledSlideshows")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class ScheduledSlideshowsController : CustomApiController
    {
        private ISlideshowRepository SlideshowRepository { get; set; }

        private ISignboardRepository SignboardRepository { get; set; }

        public ScheduledSlideshowsController(ISlideshowRepository slideshowRepository, ISignboardRepository signboardRepository)
        {
            SlideshowRepository = slideshowRepository;
            SignboardRepository = signboardRepository;
        }

        /// <summary>
        /// Get all the scheduled slideshows for a signboard
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <returns>A list of all the scheduled slideshows.</returns>
        [Route(Name = "GetScheduledSlideshows")]
        [ResponseType(typeof(List<ScheduledSlideshowModel>))]
        public IHttpActionResult GetAll(Guid companyId, Guid signboardId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null)
            {
                return NotFound();
            }

            if (signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var responseModel = signboard.ScheduledSlideshows.Select(ss => new ScheduledSlideshowModel() 
            {
                ScheduledSlideshowId = ss.Id,
                StartDate = ss.ScheduledDate,
                SlideshowId = ss.SlideshowId
            }).ToList();

            return Ok(responseModel);
        }

        /// <summary>
        /// Post a scheduled slideshow to a signboard.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <param name="model">Object containing the slideshow and start date of when you want to schedule it.</param>
        /// <returns>The scheduled slideshow model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        [ResponseType(typeof(ScheduledSlideshowModel))]
        public IHttpActionResult Post(Guid companyId, Guid signboardId, ScheduledSlideshowModel model)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null)
            {
                return NotFound();
            }

            if (signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var scheduledSlideshow = SlideshowRepository.Find(model.SlideshowId);

            if (scheduledSlideshow == null)
            {
                return BadRequest();
            }

            signboard.ScheduleSlideshow(scheduledSlideshow, model.StartDate);
            SignboardRepository.Update(signboard);

            return Created(new Uri(Url.Link("GetScheduledSlideshow", new { signboardId = signboard.Id, companyId = signboard.CompanyId, scheduledSlideshowId = scheduledSlideshow.Id })), model);
        }

        /// <summary>
        /// Post a scheduled slideshow to a signboard.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <param name="scheduledSlideshowId">The identifier of the scheduled slideshow.</param>
        [Route("{scheduledSlideshowId:Guid}")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        public IHttpActionResult Delete(Guid companyId, Guid signboardId, Guid scheduledSlideshowId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null)
            {
                return NotFound();
            }

            if (signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var scheduledSlideshow = signboard.ScheduledSlideshows.SingleOrDefault(x => x.Id == scheduledSlideshowId);

            if (scheduledSlideshow == null) 
            {
                return NotFound();
            }

            signboard.RemoveSlideshow(scheduledSlideshow);
            SignboardRepository.Update(signboard);

            return NoContent();
        }

        /// <summary>
        /// Post a scheduled slideshow to a signboard.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="signboardId">The identifier of the signboard.</param>
        /// <param name="scheduledSlideshowId">The identifier of the slideshow.</param>
        /// <returns>The scheduled slideshow model.</returns>
        [Route("{scheduledSlideshowId:Guid}", Name = "GetScheduledSlideshow")]
        [AuthoriseRoles(RoleClassification.SlideshowAuthor)]
        public IHttpActionResult Get(Guid companyId, Guid signboardId, Guid scheduledSlideshowId)
        {
            var signboard = SignboardRepository.Find(signboardId);

            if (signboard == null)
            {
                return NotFound();
            }

            if (signboard.CompanyId != companyId)
            {
                return NotFound();
            }

            var scheduledSlideshow = signboard.ScheduledSlideshows.SingleOrDefault(x => x.Id == scheduledSlideshowId);

            if (scheduledSlideshow == null)
            {
                return NotFound();
            }

            var responseModel = new ScheduledSlideshowModel()
            {
                ScheduledSlideshowId = scheduledSlideshow.Id,
                StartDate = scheduledSlideshow.ScheduledDate,
                SlideshowId = scheduledSlideshow.SlideshowId
            };

            return Ok(responseModel);
        }
    }
}