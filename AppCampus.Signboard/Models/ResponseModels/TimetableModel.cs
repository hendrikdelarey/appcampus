using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models
{
    public class TimetableModel
    {
        public OperatorModel Operator{get; set;}
        public string StopName { get; set; }
        public List<TimetableEntryModel> TimetableEntry { get; set; }
    }
}
