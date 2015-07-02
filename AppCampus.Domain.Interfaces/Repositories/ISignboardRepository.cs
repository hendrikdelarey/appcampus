using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface ISignboardRepository : IRepository<Signboard, Guid>
    {
        bool HasName(string name);

        bool HasDeviceId(Guid deviceId);

        Signboard GetByDevice(Guid deviceId);

        IReadOnlyCollection<Signboard> Get();

        IReadOnlyCollection<Signboard> Get(Guid companyId);

        bool HasScheduledSlideshow(Guid slideshowId);
    }
}