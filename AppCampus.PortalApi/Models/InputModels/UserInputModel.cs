using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The user input model.
    /// </summary>
    public class UserInputModel
    {
        /// <summary>
        /// The username of the user represented as an email address.
        /// </summary>
        [Required]
        [EmailAddress(ErrorMessage = "Username field is not a valid email address.")]
        [StringLength(50, ErrorMessage = "Username must be less than {1} characters long.")]
        public string Username { get; set; }

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