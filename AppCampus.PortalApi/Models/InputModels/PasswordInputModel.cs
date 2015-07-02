using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The password input model.
    /// </summary>
    public class PasswordInputModel
    {
        /// <summary>
        /// The current password credential for the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        /// <summary>
        /// The new password credential for the user.
        /// </summary>
        [Required]
        [StringLength(15, ErrorMessage = "Password must be between {2} and {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Password confirmation.
        /// </summary>
        [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}