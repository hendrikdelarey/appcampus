using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface IDeviceLogRepository : IRepository<DeviceLog, Guid>
    {
        Guid CreateDeviceLog(DeviceLog deviceLog);

        void UploadLogFile(Guid deviceLogId, byte[] file);

        byte[] DownloadLogFile(Guid logFileId);
    }
}
