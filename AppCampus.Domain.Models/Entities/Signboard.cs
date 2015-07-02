using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Tests.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;
using System.Linq;

namespace AppCampus.Domain.Models.Entities
{
    public class Signboard : DomainEntity<Guid>, IAggregateRoot
    {
        private string name;

        private List<Request> requests;

        public IReadOnlyCollection<Request> Requests 
        {
            get 
            {
                return requests;
            }
        }

        private ScheduledItemCollection<ScheduledSlideshow> scheduledSlideshows;

        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value", "Company name cannot be empty or null");
                }

                name = value;
            }
        }

        public bool IsShowingScreensaver { get; private set; }

        public string SoftwareVersion { get; private set; }

        public float FontFactor { get; private set; }

        public Guid CompanyId { get; private set; }

        public Guid DeviceId { get; private set; }

        public DateTime CreatedDate { get; private set; }

        public IReadOnlyScheduledItemCollection<ScheduledSlideshow> ScheduledSlideshows
        {
            get
            {
                return scheduledSlideshows;
            }
        }

        public void SetFontFactor(float value) 
        {
            if (value <= 0) 
            {
                throw new ArgumentOutOfRangeException("value", "FontFactor may not be less than zero.");
            }

            FontFactor = value;
        }

        // Create
        public Signboard(string name, Guid companyId, Guid deviceId, string softwareVersion, float fontFactor)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            Name = name;
            CompanyId = companyId;
            DeviceId = deviceId;
            CreatedDate = DateTime.UtcNow;
            scheduledSlideshows = new ScheduledItemCollection<ScheduledSlideshow>();
            requests = new List<Request>();
            SoftwareVersion = softwareVersion;
            FontFactor = fontFactor;
            IsShowingScreensaver = false;
        }

        // Hydrate
        private Signboard(Guid id, string name, Guid companyId, Guid deviceId, string softwareVersion, float fontFactor, DateTime createdDate, bool isShowingScreensaver, ICollection<ScheduledItem<ScheduledSlideshow>> scheduledItems, ICollection<Request> requests)
            : base(id)
        {
            Name = name;
            CompanyId = companyId;
            DeviceId = deviceId;
            CreatedDate = createdDate;
            scheduledSlideshows = new ScheduledItemCollection<ScheduledSlideshow>(scheduledItems);
            this.requests = requests.ToList();
            SoftwareVersion = softwareVersion;
            FontFactor = fontFactor;
            IsShowingScreensaver = isShowingScreensaver;
        }

        public static Signboard Hydrate(Guid id, string name, Guid companyId, Guid deviceId, string softwareVersion, float fontFactor, DateTime createdDate, bool isShowingScreensaver, ICollection<ScheduledItem<ScheduledSlideshow>> scheduledItems, ICollection<Request> requests)
        {
            return new Signboard(id, name, companyId, deviceId, softwareVersion, fontFactor, createdDate, isShowingScreensaver, scheduledItems, requests);
        }

        public void ChangeDevice(Guid deviceId)
        {
            DeviceId = deviceId;
        }

        public void ScheduleSlideshow(Slideshow slideshow, DateTime scheduledDate)
        {
            scheduledSlideshows.Schedule(scheduledDate, new ScheduledSlideshow(slideshow.Id, scheduledDate));
        }

        public void RemoveSlideshow(ScheduledSlideshow scheduledSlideshow)
        {
            try
            {
                scheduledSlideshows.RemoveWithValue(scheduledSlideshow);
            }
            catch (ArgumentException)
            {
                throw new ArgumentOutOfRangeException("scheduledSlideshow", "A slideshow has already been scheduled for that date");
            }
        }

        public void ToggleScreensaver() 
        {
            IsShowingScreensaver = !IsShowingScreensaver;
        }

        public void AddRequest(Request request) 
        {
            requests.Add(request);
        }

        public void CancelRequest(Request request) 
        {
            if (!requests.Contains(request)) 
            {
                throw new ArgumentOutOfRangeException("request", "The request doesn't exist.");
            }

            requests[requests.IndexOf(request)].Cancel();
        }

        public void SendRequest(Request request)
        {
            if (!requests.Contains(request))
            {
                throw new ArgumentOutOfRangeException("request", "The request doesn't exist.");
            }

            requests[requests.IndexOf(request)].Sent();
        }

        public void RequestProcessed(Request request)
        {
            if (!requests.Contains(request))
            {
                throw new ArgumentOutOfRangeException("request", "The request doesn't exist.");
            }

            requests[requests.IndexOf(request)].Processed();
        }

        public void RequestFailed(Request request)
        {
            if (!requests.Contains(request))
            {
                throw new ArgumentOutOfRangeException("request", "The request doesn't exist.");
            }

            requests[requests.IndexOf(request)].Failed();
        }
    }
}