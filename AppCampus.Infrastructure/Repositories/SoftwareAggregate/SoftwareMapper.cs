using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using System;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.SoftwareAggregate
{
    public class SoftwareMapper : IEntityMapper<Software, SoftwareTable>
    {
        public SoftwareTable CreateFrom(Software domainEntity)
        {
            return new SoftwareTable()
            {
                Id = domainEntity.Id,
                Version = domainEntity.Version.ToString(),
                CreatedDate = DateTime.UtcNow,
                Comment = domainEntity.Comment
            };
        }

        public Software CreateFrom(SoftwareTable dataEntity)
        {
            return Software.Hydrate(dataEntity.Id, Version.Parse(dataEntity.Version), dataEntity.Comment);
        }
    }
}