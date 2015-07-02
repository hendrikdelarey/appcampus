using AppCampus.Infrastructure.Models;
using System;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Infrastructure.Modules.SoftwareFiles
{
    public class SoftwareFileStorage
    {
        public byte[] GetFile(Guid softwareFileId)
        {
            SoftwareFileTable softwareFile;

            using (var context = new AppCampusContext())
            {
                softwareFile = context.SoftwareFiles.Find(softwareFileId);
            }

            return softwareFile.Binary;
        }

        public string GetFileName(Guid softwareFileId)
        {
            using (var context = new AppCampusContext())
            {
                var softwareFile = context.SoftwareFiles.Select(x => new { Id = x.Id, FileName = x.FileName }).SingleOrDefault(x => x.Id == softwareFileId);

                if (softwareFile == null)
                {
                    throw new ArgumentOutOfRangeException("softwareFileId", String.Format("There is no software file with id '{0}'", softwareFileId));
                }

                return softwareFile.FileName;
            }
        }

        public Guid SaveFile(string fileName, byte[] file)
        {
            var softwareFile = new SoftwareFileTable()
            {
                Id = CombIdentityFactory.GenerateIdentity(),
                FileName = fileName,
                Binary = file
            };

            using (var context = new AppCampusContext())
            {
                context.SoftwareFiles.Add(softwareFile);

                context.SaveChanges();
            }

            return softwareFile.Id;
        }
    }
}