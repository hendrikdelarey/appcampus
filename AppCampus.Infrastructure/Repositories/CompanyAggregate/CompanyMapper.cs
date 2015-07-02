using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.CompanyAggregate
{
    public class CompanyMapper : IEntityMapper<Company, CompanyTable>
    {
        public Company CreateFrom(CompanyTable dataEntity)
        {
            return Company.Hydrate(dataEntity.Id, dataEntity.Name);
        }

        public CompanyTable CreateFrom(Company domainEntity)
        {
            return new CompanyTable()
            {
                Id = domainEntity.Id,
                Name = domainEntity.Name
            };
        }
    }
}