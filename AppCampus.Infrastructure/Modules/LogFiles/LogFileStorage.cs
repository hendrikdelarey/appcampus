using AppCampus.Infrastructure.Models;
using System;
using Drumble.DomainDrivenArchitecture.Domain.Models;
using System.Linq;

namespace AppCampus.Infrastructure.Modules.LogFiles
{
    public class LogFileStorage
    {
        public byte[] GetFile(Guid softwareFileId)
        {
            LogFileTable logFile;

            using (var context = new AppCampusContext())
            {
                logFile = context.LogFiles.Find(softwareFileId);
            }

            return logFile.Binary;
        }

        public Guid SaveFile(byte[] file)
        {
            var logFile = new LogFileTable()
            {
                Id = CombIdentityFactory.GenerateIdentity(),
                Binary = file
            };

            using (var context = new AppCampusContext())
            {
                context.LogFiles.Add(logFile);

                context.SaveChanges();
            }

            return logFile.Id;
        }
    }
}