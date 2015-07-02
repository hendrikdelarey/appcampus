using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Tests.ValueObjects
{
    public class ScheduledItem<T> : IValueObject<ScheduledItem<T>>
    {
        public DateTime ScheduledDate { get; private set; }

        public T ScheduledValue { get; private set; }

        public ScheduledItem(DateTime scheduledDate, T scheduledValue)
        {
            ScheduledDate = scheduledDate;
            ScheduledValue = scheduledValue;
        }

        public bool Equals(ScheduledItem<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return ScheduledDate.Equals(other.ScheduledDate) && ScheduledValue.Equals(other.ScheduledValue);
        }
    }
}
