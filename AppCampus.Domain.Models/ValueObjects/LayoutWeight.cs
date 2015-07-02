using System;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class LayoutWeight : IValueObject<LayoutWeight>
    {
        public float Weight { get; private set; }

        private LayoutWeight(float weight)
        {
            if (LayoutWeight.IsValid(weight))
            {
                Weight = weight;
            }
            else
            {
                throw new ArgumentOutOfRangeException("weight", String.Format("Supplied LayoutWeight '{0}' is not a valid weight. Expected float > 0 and <= 1", weight));
            }
        }

        public static bool IsValid(float weight)
        {
            return (weight > 0.0f && weight <= 1.0f);
        }

        public bool Equals(LayoutWeight other)
        {
            return (Weight == other.Weight);
        }

        public static LayoutWeight From(float weight)
        {
            return new LayoutWeight(weight);
        }
    }
}