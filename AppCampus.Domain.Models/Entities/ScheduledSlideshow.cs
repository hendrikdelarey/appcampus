using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class ScheduledSlideshow : DomainEntity<Guid>
    {
        public Guid SlideshowId { get; private set; }

        public DateTime ScheduledDate { get; private set; }

        // Create
        public ScheduledSlideshow(Guid slideshowId, DateTime scheduledDate)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            SlideshowId = slideshowId;
            ScheduledDate = scheduledDate;
        }

        // Hydrate
        private ScheduledSlideshow(Guid scheduledSlideshowId, Guid slideshowId, DateTime scheduledDate)
            : base(scheduledSlideshowId)
        {
            SlideshowId = slideshowId;
            ScheduledDate = scheduledDate;
        }

        public static ScheduledSlideshow Hydrate(Guid scheduledSlideshowId, Guid slideshowId, DateTime scheduledDate) 
        {
            return new ScheduledSlideshow(scheduledSlideshowId, slideshowId, scheduledDate);
        }
    }
}
