using System;
using System.Windows;
using System.Windows.Media;

namespace PerformanceUtilities
{
    public static class RenderCapabilityWrapper
    {
        public static readonly DependencyProperty TierProperty =
            DependencyProperty.RegisterAttached(
                "Tier",
                typeof(int),
                typeof(PerformanceUtilities.RenderCapabilityWrapper),
                new PropertyMetadata(RenderCapability.Tier >> 16));

        public static int GetTier(DependencyObject depObj)
        {
            return (int)TierProperty.DefaultMetadata.DefaultValue;
        }
    }
}