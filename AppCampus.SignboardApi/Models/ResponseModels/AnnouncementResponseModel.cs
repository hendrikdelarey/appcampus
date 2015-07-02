using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Announcement Response model
    /// </summary>
    public class AnnouncementResponseModel
    {
        /// <summary>
        /// The Message the announcemnt will display
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// The Severity of the Announcement
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// The Date that the announcement will start showing on Signboard
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The Date that the announcement will stop showing on Signboard
        /// </summary>
        public DateTime EndDate { get; set; }

        public static AnnouncementResponseModel From(Announcement announcement)
        {
            return new AnnouncementResponseModel()
                {
                    Message = announcement.Message,
                    Severity = announcement.Severity.ToString(),
                    StartDate = announcement.StartDate,
                    EndDate = announcement.EndDate
                };
        }
    }
}