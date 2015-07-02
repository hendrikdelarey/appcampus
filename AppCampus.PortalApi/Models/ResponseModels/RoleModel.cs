using AppCampus.Domain.Models.Identity;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The role response model.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// The identifier of a role.
        /// </summary>
        public Guid RoleId { get; private set; }

        /// <summary>
        /// The name of a role.
        /// </summary>
        public string RoleName { get; private set; }

        internal static RoleModel From(ApplicationRole role)
        {
            return new RoleModel()
            {
                RoleId = role.Id,
                RoleName = role.Name
            };
        }
    }
}