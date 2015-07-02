using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Tests.ValueObjects
{
    [TestFixture]
    [Category("LayoutWidget")]
    class WidgetLayoutTests
    {
        public WidgetLayout make_WidgetLayout(LayoutWeight horizontalLayoutWeight, LayoutWeight verticalLayoutWeight)
        {
            return WidgetLayout.From(horizontalLayoutWeight, verticalLayoutWeight);
        }

        [TestCase]
        public void Comparison_SameWidgetLayout_IsEquals()
        {
            var horizontalLayoutWeight = LayoutWeight.From(1.0f);
            var verticalLayoutWeight = LayoutWeight.From(0.5f);

            var widgetLayout1 = make_WidgetLayout(horizontalLayoutWeight, verticalLayoutWeight);
            var widgetLayout2 = make_WidgetLayout(horizontalLayoutWeight, verticalLayoutWeight);

            Assert.AreEqual(widgetLayout1, widgetLayout2);
            Assert.AreEqual(widgetLayout2, widgetLayout1);
        }

        [TestCase]
        public void Comparison_DifferentWidgetLayout_IsNotEquals()
        {
            var horizontalLayoutWeight = LayoutWeight.From(1.0f);
            var verticalLayoutWeight = LayoutWeight.From(0.5f);

            var widgetLayout1 = make_WidgetLayout(horizontalLayoutWeight, verticalLayoutWeight);
            var widgetLayout2 = make_WidgetLayout(verticalLayoutWeight, horizontalLayoutWeight);

            Assert.AreNotEqual(widgetLayout1, widgetLayout2);
            Assert.AreNotEqual(widgetLayout2, widgetLayout1);
        }
    }
}
