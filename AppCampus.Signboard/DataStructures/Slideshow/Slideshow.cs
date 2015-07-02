using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.DataStructures.Widgets;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Helpers;
using AppCampus.Signboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace AppCampus.Signboard.DataStructures.Slideshow
{
    public class Slideshow
    {


        public List<Slide> Slides {get; set;}

        public Slideshow(IEnumerable<Slide> slides)
        {
            Slides = slides.ToList();
        }

        public static Slideshow From(StructureModel model) 
        {
            if(model.Slideshow == null)
            {
                return null;
            }

            try
            {
                var validSlides = model.Slideshow.Slides
                    .Where(x => 
                        x.Widgets != null 
                        && x.Widgets.Count > 0 
                        && !x.Widgets.Any(y => y.Parameters == null || y.Parameters.Count == 0))
                    .Select(s => Slide.From(s));
                Slideshow newSlideShow = new Slideshow(validSlides);
                return newSlideShow;
            }
            catch (Exception e) 
            {
                Logger.Instance.Write("Slideshow.From", LogLevel.Critical, e.Message);
                return null;
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
            Slideshow other = obj as Slideshow;
            if ((System.Object)other == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this.Equals(other);
        }


        public bool Equals(Slideshow other)
        {
            if (other == null) 
            {
                return false;
            }

            if(Slides == null && other.Slides == null)
            {
                return true;
            }

            if(other.Slides == null && Slides != null)
            {
                return false;
            }
             
            if(other.Slides != null && Slides == null)
            {
                return false;
            }

            if(other.Slides.Count != Slides.Count)
            {
                return false;
            }

            int i = 0;
            foreach (Slide slide in Slides) 
            {
                if(!slide.Equals(other.Slides[i]))
                {
                    return false;
                }
                
                i ++;
            }

            return true;
        }

        public void Start() 
        {
            foreach (Slide slide in Slides)
            {
                foreach (Widget widget in slide.Widgets)
                {
                    widget.OnStart();
                }
            }
        }

        public bool HasOnlyOneSlide() 
        {
            int count = 0;
            foreach(Slide slide in Slides)
            {
                if (slide.Widgets != null && slide.Widgets.Count > 0 && slide.Widgets.All(x => x.State == WidgetState.Ready))
                {
                    count ++;

                    if (count > 1) 
                    {
                        return false;
                    }
                }
            }

            return count == 1;
        }

        public bool HasOneOrMoreSlides() 
        {
            foreach (Slide slide in Slides)
            {
                if (slide.Widgets != null && slide.Widgets.Count > 0 && slide.Widgets.All(x => x.State == WidgetState.Ready))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public Slideshow Copy() 
        {
            var slides = new List<Slide>();
            if (this.Slides != null)
            {
                slides = this.Slides.Select(x => x.Copy()).ToList();
            }
            return new Slideshow(slides);
        }
    }
}
