using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.EditModels
{
    /// <summary>
    /// The user edit model.
    /// </summary>
    public class UserEditModel
    {
        /// <summary>
        /// The first name of the user.
        /// </summary>
        [Required]
        [StringLength(25, ErrorMessage = "First name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the user.
        /// </summary>
        [Required]
        [StringLength(25, ErrorMessage = "Last name must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }
    }
}