using AppCampus.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;

namespace AppCampus.Domain.Interfaces.Repositories
{
    public interface ISlideshowRepository : IRepository<Slideshow, Guid>
    {
        IReadOnlyCollection<Slideshow> GetAll();

        IReadOnlyCollection<Slideshow> GetActive();

        IReadOnlyCollection<Slideshow> GetByCompany(Guid companyId);

        IReadOnlyCollection<Slideshow> GetActiveByCompany(Guid companyId);

        Slideshow GetActiveBySignboard(Guid signboardId);

        bool HasSlideId(Guid slideId);

        void DeleteSlide(Guid slideshowId, Guid slideId);
    }
}