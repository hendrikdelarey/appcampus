using AppCampus.PortalApi.Validation.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Models.InputModels
{
    /// <summary>
    /// The company input model.
    /// </summary>
    public class CompanyInputModel
    {
        /// <summary>
        /// The name of the company.
        /// </summary>
        [Required]
        [UniqueCompanyName]
        public string Name { get; set; }
    }
}