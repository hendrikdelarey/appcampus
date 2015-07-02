using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using AppCampus.Infrastructure.Modules.LogFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.DeviceLogAggregate
{
    public class DeviceLogRepository : EntityFrameworkRepository<DeviceLog, Guid, DeviceLogTable>, IDeviceLogRepository
    {
        public DeviceLogRepository(IUnitOfWork unitOfWork, IEntityMapper<DeviceLog, DeviceLogTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
        }

        public Guid CreateDeviceLog(DeviceLog deviceLog) 
        {
            using (var context = NewContext())
            {
                context.Add(EntityMapper.CreateFrom(deviceLog));
                context.Commit();
            }

            return deviceLog.Id;
        }

        public void UploadLogFile(Guid deviceLogId, byte[] file)
        {
            var storage = new LogFileStorage();
            var logFileId = storage.SaveFile(file);

            using (var context = NewContext())
            {
                var deviceLog = context.Query().SingleOrDefault(x => x.Id == deviceLogId);
                if (deviceLog == null)
                {
                    throw new ArgumentOutOfRangeException("deviceLogId", String.Format("There is no such LogFile with identifier '{0}'.", deviceLogId));
                }

                deviceLog.LogFileId = logFileId;

                context.Update(deviceLog);
                context.Commit();
            }
        }

        public byte[] DownloadLogFile(Guid logFileId)
        {
            DeviceLogTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Id == logFileId);
            }

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException("logFileId", String.Format("There is no such LogFile with identifier '{0}'.", logFileId));
            }

            if (!entity.LogFileId.HasValue)
            {
                throw new InvalidOperationException("There is no log file uploaded for this device log.");
            }

            var storage = new LogFileStorage();

            return storage.GetFile(entity.LogFileId.Value);
        }
    }
}
