using AppCampus.Domain.Models.Entities;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The company response model.
    /// </summary>
    public class CompanyModel
    {
        /// <summary>
        /// The identifier of the company.
        /// </summary>
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// The name of the company.
        /// </summary>
        public string Name { get; private set; }

        internal static CompanyModel From(Company company)
        {
            return new CompanyModel()
            {
                CompanyId = company.Id,
                Name = company.Name
            };
        }
    }
}