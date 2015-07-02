using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
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
    /// Represents the announcements created by a company.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/announcements")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class AnnouncementsController : CustomApiController
    {
        private IAnnouncementRepository AnnouncementRepository { get; set; }

        public AnnouncementsController(IAnnouncementRepository announcementRepository)
        {
            AnnouncementRepository = announcementRepository;
        }

        /// <summary>
        /// Lists all the announcements created by a company.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <returns>A list of Announcements.</returns>
        [Route]
        [ResponseType(typeof(AnnouncementModel))]
        public IHttpActionResult GetAnnouncements(Guid companyId)
        {
            var announcements = AnnouncementRepository.GetByCompany(companyId).Select(x => AnnouncementModel.From(x)).ToList();

            return Ok(announcements);
        }

        /// <summary>
        /// Retrieves a specific announcement.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="announcementId">The identifier of the announcement.</param>
        /// <returns>The announcement model.</returns>
        [Route("{announcementId:Guid}", Name = "GetAnnouncement")]
        [ResponseType(typeof(AnnouncementModel))]
        public IHttpActionResult GetAnnouncement(Guid companyId, Guid announcementId)
        {
            var announcement = AnnouncementRepository.GetByCompany(companyId).SingleOrDefault(x => x.Id == announcementId);

            if (announcement == null)
            {
                return NotFound();
            }

            return Ok(AnnouncementModel.From(announcement));
        }

        /// <summary>
        /// Updates a company's announcement.
        /// </summary>
        /// <param name="announcementId">The identifier of the announcement.</param>
        /// <param name="model">The announcement input model.</param>
        [Route("{announcementId:Guid}")]
        [AuthoriseRoles(RoleClassification.AnnouncementAuthor)]
        public IHttpActionResult Put(Guid announcementId, AnnouncementInputModel model)
        {
            var announcement = AnnouncementRepository.Find(announcementId);

            if (announcement == null)
            {
                return NotFound();
            }

            announcement.SetMessage(model.Message);
            announcement.SetDateRange(model.StartDate, model.EndDate);
            announcement.SetSeverity(model.Severity);

            if (model.IsActive.HasValue && model.IsActive.Value)
            {
                announcement.Activate();
            }
            else if (model.IsActive.HasValue && !model.IsActive.Value)
            {
                announcement.Deactivate();
            }

            AnnouncementRepository.Update(announcement);

            return NoContent();
        }

        /// <summary>
        /// Creates an announcement.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="model">The company input model.</param>
        /// <returns>The created announcement.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.AnnouncementAuthor)]
        [ResponseType(typeof(AnnouncementModel))]
        public IHttpActionResult Post(Guid companyId, AnnouncementInputModel model)
        {
            Severity severity = (Severity)Enum.Parse(typeof(Severity), model.Severity, true);
            var announcement = new Announcement(companyId, model.Message, model.Name, severity, model.StartDate, model.EndDate);

            if (model.IsActive.HasValue && model.IsActive.Value)
            {
                announcement.Activate();
            }
            else if (model.IsActive.HasValue && !model.IsActive.Value)
            {
                announcement.Deactivate();
            }

            AnnouncementRepository.Add(announcement);

            return Created(new Uri(Url.Link("GetAnnouncement", new { companyId = announcement.CompanyId, announcementId = announcement.Id })), AnnouncementModel.From(announcement));
        }

        /// <summary>
        /// Delete an announcement.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="announcementId">The identifier of the announcement that you want to delete.</param>
        /// <returns>The created announcement.</returns>
        [Route("{announcementId:Guid}")]
        [AuthoriseRoles(RoleClassification.AnnouncementAuthor)]
        public IHttpActionResult Delete(Guid companyId, Guid announcementId)
        {
            var announcement = AnnouncementRepository.Find(announcementId);

            if (announcement == null)
            {
                return NotFound();
            }

            if (announcement.CompanyId != companyId)
            {
                return NotFound();
            }

            announcement.DeleteAnnouncement();

            AnnouncementRepository.Update(announcement);

            return NoContent();
        }
    }
}