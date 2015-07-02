using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class WidgetLayout : IValueObject<WidgetLayout>
    {
        public LayoutWeight HorizontalLayoutWeight { get; private set; }

        public LayoutWeight VerticalLayoutWeight { get; private set; }

        private WidgetLayout(LayoutWeight horizontalLayoutWeight, LayoutWeight verticalLayoutWeight)
        {
            HorizontalLayoutWeight = horizontalLayoutWeight;
            VerticalLayoutWeight = verticalLayoutWeight;
        }

        public bool Equals(WidgetLayout other)
        {
            return HorizontalLayoutWeight.Equals(other.HorizontalLayoutWeight) && VerticalLayoutWeight.Equals(other.VerticalLayoutWeight);
        }

        public static WidgetLayout From(LayoutWeight horizontalLayoutWeight, LayoutWeight verticalLayoutWeight)
        {
            return new WidgetLayout(horizontalLayoutWeight, verticalLayoutWeight);
        }

        public static WidgetLayout From(float horizontalLayoutWeight, float verticalLayoutWeight)
        {
            return new WidgetLayout(LayoutWeight.From(horizontalLayoutWeight), LayoutWeight.From(verticalLayoutWeight));
        }
    }
}
