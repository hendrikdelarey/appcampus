using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Models;
using System;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class Announcement
    {
        public string Message { get; private set; }

        public AnnouncementSeverity Severity { get; private set; }

        public static Announcement From(AnnouncementModel announcementModel)
        {
            return new Announcement()
            {
                Message = announcementModel.Message,
                Severity = announcementModel.Severity
            };
        }

        public override bool Equals(Object obj)
        {
            if (obj == null) 
            {
                return false;
            }

            return this.Message == ((Announcement)obj).Message && this.Severity == ((Announcement)obj).Severity;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Announcement Copy() 
        {
            return new Announcement()
            {
                Message = this.Message,
                Severity = this.Severity
            };
        }
    }
}