using AppCampus.PortalApi.Validation.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The device input model.
    /// </summary>
    public class DeviceInputModel
    {
        /// <summary>
        /// The identifier (MAC address) of the device.
        /// </summary>
        [Required]
        [ValidMacAddress]
        public string MacAddress { get; set; }

        /// <summary>
        /// The Comment associated with the Device.
        /// </summary>
        public string Comment { get; set; }
    }
}