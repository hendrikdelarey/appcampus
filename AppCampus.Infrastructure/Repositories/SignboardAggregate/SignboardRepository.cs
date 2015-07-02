using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using RefactorThis.GraphDiff;

namespace AppCampus.Infrastructure.Repositories.SignboardAggregate
{
    public class SignboardRepository : EntityFrameworkRepository<Signboard, Guid, SignboardTable>, ISignboardRepository
    {
        public SignboardRepository(IUnitOfWork unitOfWork, IEntityMapper<Signboard, SignboardTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
            RegisterUpdateMapping(map => map
                .OwnedCollection(c => c.SignboardSlideshows)
                .OwnedCollection(d => d.SignboardRequests)
            );

            RegisterQueryMapping(x => x.SignboardSlideshows);
            RegisterQueryMapping(x => x.SignboardRequests);
        }

        public override Signboard Find(Guid id)
        {
            SignboardTable entity;

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

        public bool HasName(string name)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.Name.Equals(name.Trim()));
            }
        }

        public bool HasDeviceId(Guid deviceId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.DeviceId.Equals(deviceId));
            }
        }

        public Signboard GetByDevice(Guid deviceId)
        {
            SignboardTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.DeviceId.Equals(deviceId));
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

        public IReadOnlyCollection<Signboard> Get()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(x => x.Device.DeviceStates.OrderByDescending(s => s.ChangedDate).FirstOrDefault().State == "Approved")
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public IReadOnlyCollection<Signboard> Get(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(x => x.Company.Id.Equals(companyId))
                    .Where(x => x.Device.DeviceStates.OrderByDescending(s => s.ChangedDate).FirstOrDefault().State == "Approved")
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public bool HasScheduledSlideshow(Guid slideshowId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any
                    (
                        x => x.SignboardSlideshows.Any(ss => ss.StartDate >= DateTime.UtcNow && ss.SlideshowId == slideshowId) &&
                             x.SignboardSlideshows.Where(ss => ss.StartDate < DateTime.UtcNow).OrderByDescending(y => y.StartDate).FirstOrDefault() == null ? 
                                false : x.SignboardSlideshows.Where(ss => ss.StartDate < DateTime.UtcNow).OrderByDescending(y => y.StartDate).FirstOrDefault().SlideshowId == slideshowId
                    );
            }
        }
    }
}