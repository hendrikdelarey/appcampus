using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class TimetableSchedule : IValueObject<TimetableSchedule>
    {
        public string FirstStop { get; set; }
        public string LastStop { get; set; }
        public string CorridorName { get; set; }
        public string VehicleNumber { get; set; }
        public string Status { get; set; }
        public DateTime StopArrivalTime { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime DestinationArrivalTime { get; set; }

        public static TimetableSchedule From(string firstStop, string lastStop, string corridorName, string vehicleNumber, string status, DateTime stopArrivalTime, DateTime departureTime, DateTime destinationArrivalTime) 
        {
            return new TimetableSchedule()
            {
                FirstStop = firstStop,
                LastStop = lastStop,
                CorridorName = corridorName,
                VehicleNumber = vehicleNumber,
                Status = status,
                StopArrivalTime = stopArrivalTime,
                DepartureTime = departureTime,
                DestinationArrivalTime = destinationArrivalTime
            };
        }

        public bool Equals(TimetableSchedule other)
        {
            if (other == null)
            {
                return false;
            }

            return other.LastStop == LastStop && 
                other.CorridorName == CorridorName && 
                other.VehicleNumber == VehicleNumber && other.Status == Status && 
                other.Status == LastStop && 
                other.StopArrivalTime == StopArrivalTime &&
                other.DepartureTime == DepartureTime &&
                other.DestinationArrivalTime == DestinationArrivalTime;
        }
    }
}
