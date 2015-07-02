using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Enums;
using AppCampus.PortalApi.Filters;
using AppCampus.PortalApi.Models.InputModels;
using AppCampus.PortalApi.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace AppCampus.PortalApi.Controllers
{
    /// <summary>
    /// Represents the Assigned Signboards for an Announcement.
    /// </summary>
    [RoutePrefix("api/v1/companies/{companyId:Guid}/announcements/{announcementId:Guid}/assignedsignboards")]
    [AuthoriseCompany]
    [AuthoriseRoles(RoleClassification.GeneralUser)]
    public class AssignedSignboardsController : ApiController
    {
        private IAnnouncementRepository AnnouncementRepository { get; set; }
        private ISignboardRepository SignboardRepository { get; set; }

        public AssignedSignboardsController(IAnnouncementRepository announcementRepository, ISignboardRepository signboardRepository)
        {
            AnnouncementRepository = announcementRepository;
            SignboardRepository = signboardRepository;
        }

        /// <summary>
        /// Retrieves all the Announcements created by a company.
        /// </summary>
        /// <param name="companyId">The identifier of the company.</param>
        /// <param name="announcementId">The identifier of the announcement.</param>
        /// <returns>The AssignedSignboardModel model.</returns>
        [Route(Name = "GetAssignedSignboards")]
        [ResponseType(typeof(AssignedSignboardModel))]
        public IHttpActionResult GetAssignedSignboards(Guid companyId, Guid announcementId)
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

            var signboardIds = announcement.SignboardIds;

            List<AssignedSignboardModel> responseModel = new List<AssignedSignboardModel>();
            foreach (Guid signboardId in signboardIds)
            {
                responseModel.Add(AssignedSignboardModel.From(signboardId));
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Assigns an announcement to a list of signboards
        /// </summary>
        /// <param name="companyId">The company input model.</param>
        /// <param name="announcementId">The identifier of the announcement.</param>
        /// <param name="signboardsToAssign">A list of AssignedSignboard Input Model.</param>
        /// <returns>List of AssignedSignboard Model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.AnnouncementAuthor)]
        [ResponseType(typeof(AnnouncementModel))]
        public IHttpActionResult PutAssignedSignboards(Guid companyId, Guid announcementId, ICollection<AssignedSignboardInputModel> signboardsToAssign)
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

            foreach(AssignedSignboardInputModel signboard in signboardsToAssign)
            {
                if (SignboardRepository.Find(signboard.SignboardId) == null) 
                {
                    return BadRequest();
                }
            }

            AnnouncementRepository.AssignToSignboards(announcementId, signboardsToAssign.Select(x => x.SignboardId));

            List<AssignedSignboardModel> responseModel = new List<AssignedSignboardModel>();
            foreach (Guid signboardId in announcement.SignboardIds)
            {
                responseModel.Add(AssignedSignboardModel.From(signboardId));
            }

            return Ok(responseModel);
            //return Ok(signboardsToAssign.Select(x => AssignedSignboardModel.From(x.SignboardId)).ToList());
        }

        /// <summary>
        /// Assigns an announcement to a single signboard
        /// </summary>
        /// <param name="companyId">The company input model.</param>
        /// <param name="announcementId">The identifier of the announcement.</param>
        /// <param name="signboardToAssign">The company input model.</param>
        /// <returns>The AssignedSignboard Model.</returns>
        [Route]
        [AuthoriseRoles(RoleClassification.AnnouncementAuthor)]
        [ResponseType(typeof(AnnouncementModel))]
        public IHttpActionResult PostAssignedSignboard(Guid companyId, Guid announcementId, AssignedSignboardInputModel signboardToAssign)
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

            if (SignboardRepository.Find(signboardToAssign.SignboardId) == null)
            {
                return BadRequest();
            }

            AnnouncementRepository.AssignToSignboard(announcementId, signboardToAssign.SignboardId );
            return Created(new Uri(Url.Link("GetAssignedSignboards", new { companyId = companyId, announcementId = announcementId })), AssignedSignboardModel.From(signboardToAssign.SignboardId));
        }
    }
}