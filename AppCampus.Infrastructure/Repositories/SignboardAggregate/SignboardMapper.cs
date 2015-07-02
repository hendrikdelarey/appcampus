using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Infrastructure.Models;
using AppCampus.Tests.ValueObjects;
using System;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Infrastructure.EntityFramework;

namespace AppCampus.Infrastructure.Repositories.SignboardAggregate
{
    public class SignboardMapper : IEntityMapper<Signboard, SignboardTable>
    {

        public Signboard CreateFrom(SignboardTable dataEntity)
        {
            return Signboard.Hydrate(
                dataEntity.Id,
                dataEntity.Name,
                dataEntity.CompanyId,
                dataEntity.DeviceId,
                dataEntity.SoftwareVersion,
                (float)dataEntity.FontFactor,
                dataEntity.CreatedDate,
                dataEntity.IsShowingScreensaver,
                dataEntity.SignboardSlideshows.Select(x => 
                    new ScheduledItem<ScheduledSlideshow>(x.StartDate, ScheduledSlideshow.Hydrate(x.Id, x.SlideshowId, x.StartDate))).ToList(),
                dataEntity.SignboardRequests.Select(x => Request.Hydrate(x.Id, (RequestType)Enum.Parse(typeof(RequestType), x.RequestType), x.Value, x.IsProcessed ? RequestState.Processed : x.IsSent ? RequestState.Sent : x.IsCancelled ? RequestState.Cancelled : RequestState.Created, x.CreatedDate)).ToList());
        }

        public SignboardTable CreateFrom(Signboard domainEntity)
        {
            var returnValue =  new SignboardTable()
            {
                Id = domainEntity.Id,
                CompanyId = domainEntity.CompanyId,
                Name = domainEntity.Name,
                DeviceId = domainEntity.DeviceId,
                SoftwareVersion = domainEntity.SoftwareVersion,
                FontFactor = domainEntity.FontFactor,
                CreatedDate = domainEntity.CreatedDate,
                IsShowingScreensaver = domainEntity.IsShowingScreensaver,
                SignboardRequests = domainEntity.Requests.Select(x => 
                    new SignboardRequestTable()
                    {
                        Id = Guid.NewGuid(),
                        SignboardId = domainEntity.Id,
                        IsProcessed = x.State == Domain.Models.Enums.RequestState.Processed,
                        IsSent = x.State == Domain.Models.Enums.RequestState.Sent,
                        IsCancelled = x.State == Domain.Models.Enums.RequestState.Cancelled,
                        RequestType = x.RequestType.ToString(),
                        Value = x.Value,
                        CreatedDate = x.CreatedDate
                    }).ToList(),
                SignboardSlideshows = domainEntity.ScheduledSlideshows.Items.Select(x => 
                    new SignboardSlideshowTable()
                    { 
                        Id = x.ScheduledValue.Id,
                        SignboardId = domainEntity.Id, 
                        SlideshowId = x.ScheduledValue.SlideshowId,
                        StartDate = x.ScheduledDate,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow
                    }).ToList()
            };

            return returnValue;
        }
    }
}