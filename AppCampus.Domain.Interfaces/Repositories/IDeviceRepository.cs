using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface IDeviceRepository : IRepository<Device, Guid>
    {
        Device GetByMacAddress(MacAddress macAddress);

        IReadOnlyCollection<Device> Get(Guid companyId);

        IReadOnlyCollection<Device> GetPending(Guid companyId);

        IReadOnlyCollection<Device> GetApproved(Guid companyId);

        IReadOnlyCollection<Device> GetDeclined(Guid companyId);

        IReadOnlyCollection<Device> GetBlocked(Guid companyId);
    }
}