using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The announcement input model.
    /// </summary>
    public class AnnouncementInputModel
    {
        /// <summary>
        /// The message the announcment will display.
        /// </summary>
        [Required]
        public string Message { get; set; }

        /// <summary>
        /// The name of the Announcment.
        /// Used as a reference.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The date and time the announcement is scheduled to start.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The date and time the announcement is scheduled to end.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// The severity of the announcement.
        /// </summary>
        [Required]
        public string Severity { get; set; }

        /// <summary>
        /// The active state of the announcement.
        /// </summary>
        public bool? IsActive { get; set; }
    }
}