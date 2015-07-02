using AppCampus.Domain.Models.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation.ValidationAttributes
{
    public sealed class ValidMacAddressAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return MacAddress.IsValid(value.ToString());
        }
    }
}