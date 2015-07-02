using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.AnnouncementAggregate
{
    public class AnnouncementMapper : IEntityMapper<Announcement, AnnouncementTable>
    {
        public Announcement CreateFrom(AnnouncementTable dataEntity)
        {
            Severity severity = (Severity)Enum.Parse(typeof(Severity), dataEntity.Severity, true);
            return  Announcement.Hydrate(dataEntity.Id, dataEntity.CompanyId, dataEntity.Message, dataEntity.Name, severity, dataEntity.StartDate, dataEntity.EndDate, dataEntity.SignboardAnnouncements.Select(sa => sa.SignboardId).ToList(), dataEntity.IsActive, dataEntity.IsDeleted);
        }

        public AnnouncementTable CreateFrom(Announcement domainEntity)
        {
            return new AnnouncementTable()
            {
                Id = domainEntity.Id,
                CompanyId = domainEntity.CompanyId,
                Message = domainEntity.Message,
                Name = domainEntity.Name,
                Severity = domainEntity.Severity.ToString(),
                StartDate = domainEntity.StartDate,
                EndDate = domainEntity.EndDate,
                IsActive = domainEntity.IsActive,
                IsDeleted = domainEntity.IsDeleted,
                SignboardAnnouncements = domainEntity.SignboardIds.Select(sId => new SignboardAnnouncementTable() 
                { 
                    Id = Guid.NewGuid(),
                    SignboardId = sId,
                    AnnouncementId = domainEntity.Id
                }).ToList()
            };
        }
    }
}
