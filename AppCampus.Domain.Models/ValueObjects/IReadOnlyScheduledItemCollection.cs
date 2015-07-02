using AppCampus.Tests.ValueObjects;
using System;
using System.Collections.Generic;

namespace AppCampus.Domain.Models.ValueObjects
{
    public interface IReadOnlyScheduledItemCollection<T> : IReadOnlyCollection<T>
    {
        bool HasValue(T value);

        ScheduledItem<T> GetAtDate(DateTime scheduledDate);

        ScheduledItem<T> GetByValue(T scheduledValue);

        IEnumerable<ScheduledItem<T>> Items { get; }
    }
}