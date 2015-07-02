using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The slideshow input model.
    /// </summary>
    public class SlideshowInputModel
    {
        /// <summary>
        /// The name of the slideshow.
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}