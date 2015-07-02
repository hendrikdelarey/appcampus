using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface IAnnouncementRepository : IRepository<Announcement, Guid>
    {
        IReadOnlyCollection<Announcement> GetAll();

        IReadOnlyCollection<Announcement> GetActive();

        IReadOnlyCollection<Announcement> GetByCompany(Guid companyId);

        IReadOnlyCollection<Announcement> GetActiveByCompany(Guid companyId);

        IReadOnlyCollection<Announcement> GetBySignboard(Guid signboardId);

        IReadOnlyCollection<Announcement> GetActiveBySignboard(Guid signboardId);

        void AssignToSignboards(Guid announcementId, IEnumerable<Guid> signboardIds);

        void AssignToSignboard(Guid announcementId, Guid signboardId);
    }
}
