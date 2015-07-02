using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using RefactorThis.GraphDiff;
using AppCampus.Domain.Models.Entities;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Infrastructure.Repositories.AnnouncementAggregate
{
    public class AnnouncementRepository : EntityFrameworkRepository<Announcement, Guid, AnnouncementTable>, IAnnouncementRepository
    {

        public AnnouncementRepository(IUnitOfWork unitOfWork, IEntityMapper<Announcement, AnnouncementTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
            RegisterUpdateMapping(map => map.OwnedCollection(a => a.SignboardAnnouncements));
            RegisterQueryMapping(x => x.SignboardAnnouncements);
        }

        public override Announcement Find(Guid id)
        {
            AnnouncementTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .Where(x => !x.IsDeleted)
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

        public IReadOnlyCollection<Announcement> GetAll()
        {            
            using (var context = NewContext()) 
            {
                return context.Query().Where(x => !x.IsDeleted).Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public IReadOnlyCollection<Announcement> GetActive()
        {
            using (var context = NewContext())
            {
                return context.Query().Where(x => !x.IsDeleted && x.IsActive).ToList().Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public IReadOnlyCollection<Announcement> GetByCompany(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context.Query().Where(x => !x.IsDeleted && x.CompanyId == companyId).ToList().Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public IReadOnlyCollection<Announcement> GetActiveByCompany(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context.Query().Where(x => !x.IsDeleted && x.CompanyId == companyId && x.IsActive).ToList().Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public IReadOnlyCollection<Announcement> GetBySignboard(Guid signboardId)
        {
            using (var context = NewContext())
            {
                return context.Query().Where(x => !x.IsDeleted && x.SignboardAnnouncements.Any(sa => sa.SignboardId == signboardId)).ToList().Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public IReadOnlyCollection<Announcement> GetActiveBySignboard(Guid signboardId)
        {
            using (var context = NewContext())
            {
                DateTime currentDate = DateTime.Now;
                var announcements = context.Query().Where(x => !x.IsDeleted && x.IsActive && x.SignboardAnnouncements.Any(sa => sa.SignboardId == signboardId && x.StartDate < currentDate && x.EndDate > currentDate)).ToList();

                if (announcements == null || announcements.Count == 0) 
                {
                    return new List<Announcement>();
                }

                return announcements.Select(EntityMapper.CreateFrom).ToList();
            }
        }

        public void AssignToSignboards(Guid announcementId, IEnumerable<Guid> signboardIds) 
        {
            using (var context = NewContext())
            {
                var announcement = context.Query().SingleOrDefault(x => x.Id == announcementId);

                announcement.SignboardAnnouncements.Clear();

                foreach(Guid signboardId in signboardIds)
                {
                        announcement.SignboardAnnouncements.Add(
                            new SignboardAnnouncementTable()
                            {
                                Id = CombIdentityFactory.GenerateIdentity(),
                                AnnouncementId = announcementId,
                                SignboardId = signboardId
                            });
                }
                context.Update(announcement);
                context.Commit();
            }
        }

        public void AssignToSignboard(Guid announcementId, Guid signboardId) 
        {
            using (var context = NewContext())
            {
                var announcement = context.Query().SingleOrDefault(x => x.Id == announcementId);

                announcement.SignboardAnnouncements.Add(
                    new SignboardAnnouncementTable()
                    {
                        Id = CombIdentityFactory.GenerateIdentity(),
                        AnnouncementId = announcementId,
                        SignboardId = signboardId
                    });

                context.Update(announcement);
                context.Commit();
            }
        }
    }
}
