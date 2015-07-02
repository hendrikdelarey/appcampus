using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Models;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.DeviceAggregate
{
    public class DeviceRepository : EntityFrameworkRepository<Device, Guid, DeviceTable>, IDeviceRepository
    {
        public DeviceRepository(IUnitOfWork unitOfWork, IEntityMapper<Device, DeviceTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
            RegisterQueryMapping(x => x.DeviceStates);
            RegisterUpdateMapping(x => x.OwnedCollection(map => map.DeviceStates));
        }

        public override Device Find(Guid id)
        {
            DeviceTable entity;

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

        //[LogControllerAttribute]
        public Device GetByMacAddress(MacAddress macAddress)
        {
            DeviceTable entity;
            string address = macAddress.ToString();

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.MacAddress.Equals(address));
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

        public IReadOnlyCollection<Device> Get(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(x => x.CompanyId.Equals(companyId))
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }


        public IReadOnlyCollection<Device> GetPending(Guid companyId)
        {
            return GetByState(companyId, "Pending");
        }

        public IReadOnlyCollection<Device> GetApproved(Guid companyId)
        {
            return GetByState(companyId, "Approved");
        }

        public IReadOnlyCollection<Device> GetDeclined(Guid companyId)
        {
            return GetByState(companyId, "Declined");
        }

        public IReadOnlyCollection<Device> GetBlocked(Guid companyId)
        {
            return GetByState(companyId, "Blocked");
        }

        private IReadOnlyCollection<Device> GetByState(Guid companyId, string state)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(x => x.CompanyId.Equals(companyId))
                    .Where(x => x.DeviceStates.OrderByDescending(s => s.ChangedDate).FirstOrDefault().State == state)
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }
    }
}