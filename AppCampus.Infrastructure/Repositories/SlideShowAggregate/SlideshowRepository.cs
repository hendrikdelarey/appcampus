using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Interfaces.Repositories;
using AppCampus.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Interfaces;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;
using RefactorThis.GraphDiff;

namespace AppCampus.Infrastructure.Repositories.SlideshowAggregate
{
    public class SlideshowRepository : EntityFrameworkRepository<Slideshow, Guid, SlideshowTable>, ISlideshowRepository
    {
        public SlideshowRepository(IUnitOfWork unitOfWork, IEntityMapper<Slideshow, SlideshowTable> entityMapper)
            : base(unitOfWork, entityMapper)
        {
            RegisterUpdateMapping(map => map
                .OwnedCollection(c => c.Slides, with => with
                    .OwnedCollection(sw => sw.SlideWidgets, swWith => swWith
                        .OwnedCollection(p => p.Parameters))));

            RegisterQueryMapping(x => x.Slides);
            RegisterQueryMapping(x => x.Slides.Select(s => s.SlideWidgets));
            RegisterQueryMapping(x => x.Slides.Select(s => s.SlideWidgets.Select(w => w.Parameters)));
        }

        public override Slideshow Find(Guid id)
        {
            SlideshowTable entity;

            using (var context = NewContext())
            {
                entity = context
                    .Query()
                    .SingleOrDefault(x => x.Id == id && x.IsDeleted == false);
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

        public IReadOnlyCollection<Slideshow> GetAll()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(slideshow => !slideshow.IsDeleted)
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public IReadOnlyCollection<Slideshow> GetActive()
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(slideshow => !slideshow.IsDeleted)
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public IReadOnlyCollection<Slideshow> GetByCompany(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(slideshow => !slideshow.IsDeleted && slideshow.CompanyId == companyId)
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public IReadOnlyCollection<Slideshow> GetActiveByCompany(Guid companyId)
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Where(slideshow => !slideshow.IsDeleted && slideshow.CompanyId == companyId)
                    .ToList()
                    .Select(EntityMapper.CreateFrom)
                    .ToList();
            }
        }

        public Slideshow GetActiveBySignboard(Guid signboardId)
        {
            using (var context = NewContext())
            {
                var slideshow = context
                    .Query()
                    .SelectMany(x => x.SignboardSlideshows)
                    .Where(x => x.SignboardId == signboardId && x.IsActive && x.StartDate < DateTime.UtcNow)
                    .OrderByDescending(x => x.StartDate)
                    .Select(x => x.Slideshow)
                    .FirstOrDefault();

                if(slideshow == null)
                {
                    return null;
                }

                return Find(slideshow.Id);
            }
        }

        public bool HasSlideId(Guid slideId) 
        {
            using (var context = NewContext())
            {
                return context
                    .Query()
                    .Any(x => x.Slides.Any(y => y.Id == slideId));
            }
        }

        public void DeleteSlide(Guid slideshowId, Guid slideId) 
        {
            using (var context = NewContext())
            {
                var slideshow = context.Query().SingleOrDefault(x => x.Id == slideshowId);
                slideshow.Slides.SingleOrDefault(x => x.Id == slideId).IsDeleted = true;
                context.Update(slideshow);
                context.Commit();
            }
        }
    }
}
