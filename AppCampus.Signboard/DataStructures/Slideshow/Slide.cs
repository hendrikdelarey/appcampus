using AppCampus.Signboard.DataStructures.Widgets;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class Slide
    {
        public int DurationInSeconds { get; set; }

        public List<Widget> Widgets { get; set; }

        public Color BackgroundColour { get; set; }

        public static Slide From(SlideModel model)
        {
            var backgroundColour = (Color)ColorConverter.ConvertFromString(model.BackgroundColour);
            var durationInSeconds = model.DurationInSeconds;

            var widgets = model.Widgets.Select(w => Widget.From(w));

            return new Slide(backgroundColour, durationInSeconds, widgets);
        }

        public Slide(Color backGroundColor, int durationInSectionds, IEnumerable<Widget> widgets)
        {
            this.BackgroundColour = backGroundColor;
            this.DurationInSeconds = durationInSectionds;
            this.Widgets = widgets.ToList();
        }

        public void Render(Grid grid)
        {
            grid.Children.Clear();
            grid.RowDefinitions.Clear();
            grid.ColumnDefinitions.Clear();

            InitialiseSlide(grid);
        }

        private void InitialiseSlide(Grid grid)
        {
            // set background of slide
            SolidColorBrush backgroundColour = new SolidColorBrush(BackgroundColour);
            grid.Background = backgroundColour;

            foreach (Widget widget in Widgets)
            {
                UIElement widgetToDisplay = widget.GetUiElement();

                if (widgetToDisplay == null)
                {
                    return;
                }

                grid.Children.Add(widgetToDisplay);
            }
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            Slide p = obj as Slide;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.Equals(p);
        }

        public bool Equals(Slide other)
        {
            if (DurationInSeconds != other.DurationInSeconds)
            {
                return false;
            }

            if (BackgroundColour != other.BackgroundColour)
            {
                return false;
            }

            if (Widgets == null && other.Widgets == null)
            {
                return true;
            }

            if (Widgets == null && other.Widgets != null)
            {
                return false;
            }

            if (Widgets != null && other.Widgets == null)
            {
                return false;
            }

            if (Widgets.Count != other.Widgets.Count)
            {
                return false;
            }

            int i = 0;
            foreach (Widget widget in Widgets)
            {
                if (!widget.Equals(other.Widgets[i]))
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        public bool IsValid()
        {
            if (Widgets == null || Widgets.Count == 0)
            {
                return false;
            }

            foreach (Widget widget in Widgets)
            {
                if (widget.State != WidgetState.Ready)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Slide Copy()
        {
            var widgets = new List<Widget>();
            if (this.Widgets != null)
            {
                widgets = this.Widgets.Select(x => x.Copy()).ToList();
            }
            return new Slide(
             this.BackgroundColour,
             this.DurationInSeconds,
             widgets
             );
        }
    }
}