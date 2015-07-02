using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Slideshow : DomainEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }

        private string name;

        private readonly List<Slide> slides;

        public bool IsDeleted { get; private set; }

        public string Name 
        { 
            get
            {
                return name;
            }
            private set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException("value", "Slideshow Name may not be null or empty");
                }
                else
                {
                    name = value;
                }
            }
        }

        public IReadOnlyCollection<Slide> Slides
        {
            get
            {
                return slides;
            }
        }

        public Slideshow(string name, Guid companyId)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            slides = new List<Slide>();
            Name = name;
            CompanyId = companyId;
            IsDeleted = false;
        }

        private Slideshow(Guid id, string name, Guid companyId, bool isDeleted)
            : base(id)
        {
            slides = new List<Slide>();
            Name = name;
            CompanyId = companyId;
            IsDeleted = isDeleted;
        }

        public static Slideshow Hydrate(Guid id, string name, Guid companyId, bool isDeleted) 
        {
            return new Slideshow(id, name, companyId, isDeleted);
        }

        public void Insert(Slide slide)
        {
            if (slide == null) 
            {
                throw new ArgumentNullException("slide", "Slide may not be null.");
            }
            slides.Add(slide);
        }

        public void InsertBefore(Slide newSlide, Slide referenceSlide)
        {
            if (!slides.Contains(referenceSlide))
            {
                throw new ArgumentOutOfRangeException("referenceSlide", "Slide does not exist in slide show");
            }

            slides.Insert(slides.IndexOf(referenceSlide), newSlide);
        }

       public void InsertAfter(Slide newSlide, Slide referenceSlide)
        {
            if (!slides.Contains(referenceSlide))
            {
                throw new ArgumentOutOfRangeException("referenceSlide", "Slide does not exist in slide show");
            }

            slides.Insert(slides.IndexOf(referenceSlide) + 1, newSlide);
        }

        public void MoveBefore(Slide slide, Slide referenceSlide)
       {
           if (!slides.Contains(slide))
           {
               throw new ArgumentOutOfRangeException("slide", "Slide does not exist in slide show");
           }

           if (!slides.Contains(referenceSlide))
           {
               throw new ArgumentOutOfRangeException("referenceSlide", "Slide does not exist in slide show");
           }

           slides.Remove(slide);
           InsertBefore(slide, referenceSlide);
           
       }

        public void MoveAfter(Slide slide, Slide referenceSlide)
        {
            if (!slides.Contains(slide))
            {
                throw new ArgumentOutOfRangeException("slide", "Slide does not exist in slide show");
            }

            if (!slides.Contains(referenceSlide))
            {
                throw new ArgumentOutOfRangeException("referenceSlide", "Slide does not exist in slide show");
            }

            slides.Remove(slide);
            InsertAfter(slide, referenceSlide);
            
        }

        public void RemoveSlide(Slide slide)
        {
            if(!slides.Contains(slide))
            {
                throw new ArgumentOutOfRangeException("slide", "Slide does not exist in slide show");
            }

            slides.Remove(slide);
        }

        public void CopyFrom(Slideshow slideShow)
        {
            this.Name = slideShow.Name;

            foreach (Slide slide in slideShow.Slides)
            {
                Slide slideCopy = new Slide(slide.Duration, slide.Transition, slide.Name);

                foreach (Widget widget in slide.Widgets)
                {
                    Widget widgetCopy = new Widget(widget.WidgetDefinitionId, widget.Position);

                    foreach (Parameter parameter in widget.Parameters)
                    {
                        widgetCopy.AssignParameter(Parameter.From(parameter));
                    }

                    slideCopy.AddWidget(widgetCopy);
                }

                this.Insert(slideCopy);
            }
        }

        public void DeleteSlideshow() 
        {
            IsDeleted = true;
        }

        public void UpdateName(string newName) 
        {
            Name = newName;
        }
    }
}