using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Domain.Models.Entities;
using AppCampus.Infrastructure.Models;
using AppCampus.Infrastructure.Modules.SoftwareFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.SoftwareAggregate
{
    public class SoftwareRepository : EntityFrameworkRepository<Software, Guid, SoftwareTable>, ISoftwareRepository
    {
        public SoftwareRepository(IUnitOfWork unitOfWork, IEntityMapper<Software, SoftwareTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
        }

        public override Software Find(Guid id)
        {
            SoftwareTable entity;

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

        public IReadOnlyCollection<Software> GetAll()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public string GetFileName(Guid softwareId)
        {
            SoftwareTable software;

            using (var context = NewContext())
            {
                software = context.Query().SingleOrDefault(x => x.Id == softwareId);

                if (software == null)
                {
                    throw new ArgumentOutOfRangeException("softwareId", String.Format("There is no such software with identifier '{0}'.", softwareId));
                }

                if (!software.SoftwareFileId.HasValue)
                {
                    throw new InvalidOperationException("There is no software file associated with this version.");
                }
            }

            var storage = new SoftwareFileStorage();

            return storage.GetFileName(software.SoftwareFileId.Value);
        }

        public void UploadSoftware(Guid softwareId, string fileName, byte[] file)
        {
            var storage = new SoftwareFileStorage();

            var softwareFileId = storage.SaveFile(fileName, file);

            using (var context = NewContext())
            {
                var entity = context.Query().SingleOrDefault(x => x.Id == softwareId);

                if (entity == null)
                {
                    throw new ArgumentOutOfRangeException("softwareId", String.Format("There is no such software with identifier '{0}'.", softwareId));
                }

                entity.SoftwareFileId = softwareFileId;

                context.Update(entity);
                context.Commit();
            }
        }

        public byte[] DownloadSoftware(Guid softwareId)
        {
            SoftwareTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Id == softwareId);
            }

            if (entity == null)
            {
                throw new ArgumentOutOfRangeException("softwareId", String.Format("There is no such software with identifier '{0}'.", softwareId));
            }

            if (!entity.SoftwareFileId.HasValue)
            {
                throw new InvalidOperationException("There is no software file associated with this version.");
            }

            var storage = new SoftwareFileStorage();

            return storage.GetFile(entity.SoftwareFileId.Value);
        }
    }
}