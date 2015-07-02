using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The announcement response model.
    /// </summary>
    public class AnnouncementModel
    {
        /// <summary>
        /// The identifier of the announcement.
        /// </summary>
        public Guid AnnouncementId { get; private set; }

        /// <summary>
        /// A List of all teh SignbordIds that are attached with this announcement.
        /// </summary>
        public ICollection<Guid> SignboardIds { get; private set; }

        /// <summary>
        /// The message the announcment will display.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// The name of the announcment.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The date and time the announcement is scheduled to start.
        /// </summary>
        public DateTime StartDate { get; private set; }

        /// <summary>
        /// The date and time the announcement is scheduled to end.
        /// </summary>
        public DateTime EndDate { get; private set; }

        /// <summary>
        /// The severity of the announcment.
        /// </summary>
        public string Severity { get; private set; }

        /// <summary>
        /// Enable or disable the announcenent.
        /// </summary>
        public bool IsActive { get; private set; }

        public static AnnouncementModel From(Announcement announcement)
        {
            return new AnnouncementModel()
            {
                AnnouncementId = announcement.Id,
                SignboardIds = announcement.SignboardIds.ToList(),
                Message = announcement.Message,
                Name = announcement.Name,
                StartDate = announcement.StartDate,
                EndDate = announcement.EndDate,
                Severity = announcement.Severity.ToString(),
                IsActive = announcement.IsActive
            };
        }
    }
}