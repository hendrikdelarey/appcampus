using System;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The user role input model.
    /// </summary>
    public class UserRoleInputModel
    {
        /// <summary>
        /// The identifier of a role.
        /// </summary>
        public Guid RoleId { get; set; }
    }
}