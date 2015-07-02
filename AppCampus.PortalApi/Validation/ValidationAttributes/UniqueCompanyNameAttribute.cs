using AppCampus.Domain.Interfaces.Repositories;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation.ValidationAttributes
{
    public sealed class UniqueCompanyNameAttribute : ValidationAttribute
    {
        public ICompanyRepository CompanyRepository { get; set; }

        public UniqueCompanyNameAttribute()
            : base("Company name must be unique.")
        {
            CompanyRepository = (ICompanyRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ICompanyRepository));
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }

            return !CompanyRepository.HasName(value.ToString().Trim());
        }
    }
}