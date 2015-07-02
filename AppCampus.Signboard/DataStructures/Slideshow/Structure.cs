using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class Structure
    {
        public Slideshow Slideshow { get; set; }

        public List<Announcement> Announcements { get; set; }

        public int PollingIntervalInSeconds { get; set; }

        public bool RestartDevice { get; set; }

        public bool IsShowScreensaver { get; set; }

        public bool SoftwareUpdate { get; set; }

        public float FontSizeFactor = 1;

        public List<Request> Requests { get; set; }

        public static Structure From(StructureModel model) 
        {
            if (model.IsEmpty()) 
            {
                return new Structure();
            }

            return new Structure()
            {
                Announcements = model.Announcements.Select(x => Announcement.From(x)).ToList(),
                PollingIntervalInSeconds = model.PollingIntervalInSeconds,
                Slideshow = Slideshow.From(model),
                Requests = model.Requests.Select(x => Request.From(x.RequestId, x.RequestType, x.Value, x.ValueType)).ToList(),
                RestartDevice = model.Requests.Any(x => x.RequestType == RequestType.RestartDevice),
                SoftwareUpdate = model.Requests.Any(x => x.RequestType == RequestType.SoftwareUpdate),
                IsShowScreensaver = model.IsShowingScreensaver,
                FontSizeFactor = model.FontFactor
            };
        }

        public Structure Copy() 
        {
            return new Structure()
            {
                Slideshow = this.Slideshow == null ? null : this.Slideshow.Copy(),
                Announcements = this.Announcements == null || this.Announcements.Count == 0 || this.Announcements.Select(x => x.Copy()) == null ? null : this.Announcements.Select(x => x.Copy()).ToList(),
                PollingIntervalInSeconds = this.PollingIntervalInSeconds,
                RestartDevice = this.RestartDevice,
                IsShowScreensaver = this.IsShowScreensaver,
                FontSizeFactor = this.FontSizeFactor,
                SoftwareUpdate = this.SoftwareUpdate,
                Requests = this.Requests == null || this.Requests.Count == 0 || this.Requests.Select(x => x.Copy()) == null ? null : this.Requests.Select(x => x.Copy()).ToList()
            };
        }
    }
}
