using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppCampus.Signboard.Models
{
    public class TimetableEntryModel : IComparable<TimetableEntryModel>
    {
        public string Destination { get; set; }
        public string RouteName { get; set; }
        public DateTime DepartureTime { get; set; }

        public string DepartureTimeDisplayString
        {
            get
            {
                return DepartureTime.ToLocalTime().ToString("HH:mm");
            }
        }

        public int CompareTo(TimetableEntryModel other)
        {
            return DateTime.Compare(this.DepartureTime, other.DepartureTime);
        }
    }
}
