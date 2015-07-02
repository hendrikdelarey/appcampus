using AppCampus.Domain.Models.ValueObjects;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidDurationInSecondsAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return Duration.IsValid(Int32.Parse(value.ToString()));
        }
    }
}