using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models
{
    public class SlideModel
    {
        public string BackgroundColour { get; set; }
        public int DurationInSeconds { get; set; }
        public List<WidgetModel> Widgets { get; set; }
    }
}
