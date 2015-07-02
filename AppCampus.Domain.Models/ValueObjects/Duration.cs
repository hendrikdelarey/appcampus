using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Duration : IValueObject<Duration>
    {
        public int Seconds { get; private set; }

        private Duration(int seconds)
        {
            if (Duration.IsValid(seconds))
            {
                Seconds = seconds;
            }
            else
            {
                throw new ArgumentOutOfRangeException("seconds", String.Format("Supplied Duration '{0}' is not a valid duration. Expected integer greater than or equal to zero", seconds));
            }
        }

        public static bool IsValid(int seconds)
        {
            return seconds >= 0;
        }

        public bool Equals(Duration other)
        {
            return (Seconds == other.Seconds);
        }

        public static Duration From(int seconds)
        {
            return new Duration(seconds);
        }
    }
}
