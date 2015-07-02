using AppCampus.Domain.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidHexColourAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Colour.IsColourFormat(value.ToString());
        }
    }
}