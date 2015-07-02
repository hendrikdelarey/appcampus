using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models.InputModels
{
    public class SlideHealth
    {
        public int SlideIndex { get; set; }
        public List<WidgetHealth> WidgetHealth { get; set; }
    }
}
