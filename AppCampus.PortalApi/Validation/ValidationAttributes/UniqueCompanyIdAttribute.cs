using AppCampus.Domain.Interfaces.Repositories;
using System;
using System.ComponentModel.DataAnnotations;

namespace AppCampus.PortalApi.Validation.ValidationAttributes
{
    public sealed class UniqueCompanyIdAttribute : ValidationAttribute
    {
        public ICompanyRepository CompanyRepository { get; set; }

        public UniqueCompanyIdAttribute()
            : base("Company Id does not exist.")
        {
            CompanyRepository = (ICompanyRepository)Startup.HttpConfiguration.DependencyResolver.GetService(typeof(ICompanyRepository));
        }

        public override bool IsValid(object value)
        {
            return CompanyRepository.Find(Guid.Parse(value.ToString())) == null;
        }
    }
}