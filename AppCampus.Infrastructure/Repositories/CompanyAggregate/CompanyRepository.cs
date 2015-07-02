using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.CompanyAggregate
{
    public class CompanyRepository : EntityFrameworkRepository<Company, Guid, CompanyTable>, ICompanyRepository
    {
        public CompanyRepository(IUnitOfWork unitOfWork, IEntityMapper<Company, CompanyTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
        }

        public override Company Find(Guid id)
        {
            CompanyTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Id == id);
            }

            if (entity == null)
            {
                return null;
            }
            else
            {
                return EntityMapper.CreateFrom(entity);
            }
        }

        public Company FindByName(string name)
        {
            CompanyTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Name == name);
            }

            if (entity == null)
            {
                return null;
            }
            else
            {
                return EntityMapper.CreateFrom(entity);
            }
        }

        public bool HasName(string name)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.Name.Equals(name.Trim()));
            }
        }

        public IReadOnlyCollection<Company> GetAll()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }
    }
}