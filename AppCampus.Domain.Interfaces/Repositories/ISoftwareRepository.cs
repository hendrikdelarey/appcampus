using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface ISoftwareRepository : IRepository<Software, Guid>
    {
        IReadOnlyCollection<Software> GetAll();

        string GetFileName(Guid softwareId);

        void UploadSoftware(Guid softwareId, string fileName, byte[] file);

        byte[] DownloadSoftware(Guid softwareId);
    }
}