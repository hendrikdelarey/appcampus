using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Models;
using System;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Models;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.SignboardAggregate
{
    public class DeviceMapper : IEntityMapper<Device, DeviceTable>
    {
        public Device CreateFrom(DeviceTable dataEntity)
        {
            return Device.Hydrate(
                dataEntity.Id,
                dataEntity.CompanyId,
                MacAddress.From(dataEntity.MacAddress),
                dataEntity.Comment,
                dataEntity.DeviceStates.Select(x => new DeviceStateChange(ParseEnum<DeviceState>(x.State), x.ChangedDate)).ToList(),
                dataEntity.CreatedDate);
        }

        public DeviceTable CreateFrom(Device domainEntity)
        {
            return new DeviceTable()
            {
                Id = domainEntity.Id,
                CompanyId = domainEntity.CompanyId,
                MacAddress = domainEntity.MacAddress.ToString(),
                CreatedDate = domainEntity.CreatedDate,
                Comment = domainEntity.Comment,
                DeviceStates = domainEntity.DeviceStateChanges.Select(x => new DeviceStateTable() { Id=CombIdentityFactory.GenerateIdentity(), DeviceId = domainEntity.Id, State = x.State.ToString(), ChangedDate = x.ChangedDate }).ToList()
            };
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}