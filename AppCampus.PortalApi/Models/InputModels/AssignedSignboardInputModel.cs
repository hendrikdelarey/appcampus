using AppCampus.PortalApi.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The AssignedSignboard input model.
    /// </summary>
    public class AssignedSignboardInputModel
    {
        /// <summary>
        /// Indentifier of the Signboard you want an announcement to show on
        /// </summary>
        [Required]
        [ValidSignboardId]
        public Guid SignboardId { get; set; }
    }
}