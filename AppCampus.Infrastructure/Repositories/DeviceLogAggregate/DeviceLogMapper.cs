using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.DeviceLogAggregate
{
    public class DeviceLogMapper : IEntityMapper<DeviceLog, DeviceLogTable>
    {
        public DeviceLogTable CreateFrom(DeviceLog domainEntity)
        {
            return new DeviceLogTable()
            {
                Id = domainEntity.Id,
                FileName = domainEntity.FileName
            };
        }

        public DeviceLog CreateFrom(DeviceLogTable dataEntity)
        {
            return DeviceLog.Hydrate(dataEntity.Id, dataEntity.FileName);
        }
    }
}
