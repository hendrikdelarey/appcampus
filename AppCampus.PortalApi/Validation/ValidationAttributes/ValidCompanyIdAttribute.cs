using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation
{
    public sealed class ValidCompanyIdAttribute : ValidationAttribute
    {
        public ICompanyRepository CompanyRepository { get; set; }

        public ValidCompanyIdAttribute()
            : base("Company does not exist.")
        {
            CompanyRepository = (ICompanyRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ICompanyRepository));
        }

        public override bool IsValid(object value)
        {
            return CompanyRepository.Find(Guid.Parse(value.ToString())) != null;
        }
    }
}