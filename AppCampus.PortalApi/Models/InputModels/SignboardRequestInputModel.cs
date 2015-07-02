using AppCampus.Domain.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The signboard request input model.
    /// </summary>
    public class SignboardRequestInputModel
    {
        /// <summary>
        /// The request type to the Signboard.
        /// </summary>
        [Required]
        public RequestType RequestType { get; set; }

        /// <summary>
        /// If the request needs a value this is where the value is stored.
        /// </summary>
        public string Value { get; set; }
    }
}