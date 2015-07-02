using AppCampus.Signboard.Models.ResponseModels;
using System.Collections.Generic;

namespace AppCampus.Signboard.Models
{
    public class StructureModel
    {
        public SlideshowModel Slideshow { get; set; }

        public List<AnnouncementModel> Announcements { get; set; }

        public int PollingIntervalInSeconds { get; set; }

        public List<RequestModel> Requests { get; set; }

        public bool IsShowingScreensaver { get; set; }

        public float FontFactor { get; set; }

        public bool IsEmpty()
        {
            return Slideshow == null;
        }
    }
}