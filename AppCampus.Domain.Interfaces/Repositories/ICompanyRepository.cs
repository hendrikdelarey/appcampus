using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface ICompanyRepository : IRepository<Company, Guid>
    {
        Company FindByName(string name);

        bool HasName(string name);

        IReadOnlyCollection<Company> GetAll();
    }
}