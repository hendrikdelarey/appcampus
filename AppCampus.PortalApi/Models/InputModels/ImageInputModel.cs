using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.QueryModels
{
    /// <summary>
    /// The image input model.
    /// </summary>
    public class ImageInputModel
    {
        /// <summary>
        /// The Base64 image string.
        /// </summary>
        [Required]
        public string Base64Image { get; set; }

        /// <summary>
        /// The Name of the Image.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}