using AppCampus.Domain.Models.Identity;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The user role response model.
    /// </summary>
    public class UserRoleModel
    {
        /// <summary>
        /// The identifier of a role.
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// The name of a role.
        /// </summary>
        public string RoleName { get; private set; }

        internal static UserRoleModel From(ApplicationRole role)
        {
            return new UserRoleModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}