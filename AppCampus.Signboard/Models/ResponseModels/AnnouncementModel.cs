using AppCampus.Signboard.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models
{
    public class AnnouncementModel
    {
        public string Message { get; set; }
        public AnnouncementSeverity Severity { get; set; }
    }
}
