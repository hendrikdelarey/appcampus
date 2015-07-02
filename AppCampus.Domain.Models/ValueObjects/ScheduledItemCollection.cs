using AppCampus.Tests.ValueObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class ScheduledItemCollection<T> : IReadOnlyScheduledItemCollection<T>
    {
        private List<ScheduledItem<T>> scheduledItems;

        public ScheduledItemCollection()
        {
            scheduledItems = new List<ScheduledItem<T>>();
        }

        public ScheduledItemCollection(ICollection<ScheduledItem<T>> newScheduledItems)
        {
            scheduledItems = newScheduledItems.ToList();
        }

        public void Schedule(DateTime scheduledDate, T item)
        {

            if (scheduledItems.Any(x => x.ScheduledDate.Equals(scheduledDate)))
            {
                throw new ArgumentException("Cannot schedule values on the same date", "item");
            }

            scheduledItems.Add(new ScheduledItem<T>(scheduledDate, item));
        }

        public void RemoveWithValue(T value)
        {
            scheduledItems.RemoveAll(x => x.ScheduledValue.Equals(value));
        }

        public void RemoveAtDate(DateTime date)
        {
            scheduledItems.RemoveAll(x => x.ScheduledDate.Equals(date));
        }

        public bool HasValue(T value)
        {
            return scheduledItems.Any(x => x.ScheduledValue.Equals(value));
        }

        public ScheduledItem<T> GetAtDate(DateTime scheduledDate)
        {
            var scheduledItem = scheduledItems.FirstOrDefault(x => x.ScheduledDate.Equals(scheduledDate));

            if (scheduledItem == null)
            {
                throw new ArgumentOutOfRangeException(String.Format("No item scheduled at date '{0}'", scheduledDate));
            }
            else
            {
                return scheduledItem;
            }
        }

        public ScheduledItem<T> GetByValue(T scheduledValue)
        {
            var scheduledItem = scheduledItems.FirstOrDefault(x => x.ScheduledValue.Equals(scheduledValue));

            if (scheduledItem == null)
            {
                throw new ArgumentOutOfRangeException(String.Format("No item scheduled with value '{0}'", scheduledValue));
            }
            else
            {
                return scheduledItem;
            }
        }

        public int Count
        {
            get { return scheduledItems.Count(); }
        }

        public IEnumerable<ScheduledItem<T>> Items
        {
            get
            {
                return scheduledItems;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return scheduledItems.Select(x => x.ScheduledValue).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return scheduledItems.GetEnumerator();
        }
    }
}