using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidUpdateSlideshowOperationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (value.ToString().ToUpper().Equals("MOVEAFTER") || value.ToString().ToUpper().Equals("MOVEBEFORE"));
        }
    }
}