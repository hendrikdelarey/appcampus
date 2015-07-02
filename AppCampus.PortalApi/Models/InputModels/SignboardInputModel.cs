using AppCampus.PortalApi.Validation.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The signboard input model.
    /// </summary>
    public class SignboardInputModel
    {
        /// <summary>
        /// The name of the signboard.
        /// </summary>
        [Required]
        [UniqueSignboardName]
        public string Name { get; set; }

        /// <summary>
        /// The identifier of the associated device.
        /// </summary>
        [Required]
        [ValidMacAddress]
        public string MacAddress { get; set; }
    }
}