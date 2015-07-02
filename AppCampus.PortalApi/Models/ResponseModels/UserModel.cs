using AppCampus.Domain.Models.Identity;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The user model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The identifier of the user.
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// The username of the user.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// The first name of the user.
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        public string LastName { get; private set; }

        /// <summary>
        /// The system-generated password. ONLY available in debug build for use by integration tests.
        /// </summary>
        public string Password { get; private set; }

        internal static UserModel From(ApplicationUser user)
        {
            return new UserModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        internal static UserModel From(ApplicationUser user, string password)
        {
            return new UserModel()
            {
                UserId = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = password
            };
        }
    }
}