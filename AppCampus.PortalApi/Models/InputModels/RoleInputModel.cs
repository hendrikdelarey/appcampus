using System;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The role input model.
    /// </summary>
    public class RoleInputModel
    {
        /// <summary>
        /// The identifier of a role.
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// The name of a role.
        /// </summary>
        public string RoleName { get; set; }
    }
}