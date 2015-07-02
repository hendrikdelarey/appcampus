using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Slide : DomainEntity<Guid>
    {
        public Duration Duration { get; private set; }

        public Transition Transition { get; private set; }

        public Colour BackgroundColour { get; private set; }

        public string Name { get; private set; }

        public bool IsActive { get; private set; }

        public bool IsDeleted { get; private set; }

        private readonly List<Widget> widgets;

        public IReadOnlyCollection<Widget> Widgets
        {
            get
            {
                return widgets;
            }
        }

        public Slide(Duration duration, Transition transition, string name)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            widgets = new List<Widget>();
            Duration = duration;
            Transition = transition;
            BackgroundColour = new Colour("#FFFFFF");
            Name = name;
        }

        public Slide(Colour colour, Duration duration, Transition transition, string name)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            widgets = new List<Widget>();
            Duration = duration;
            Transition = transition;
            BackgroundColour = colour;
            Name = name;
            IsActive = false;
            IsDeleted = false;
        }

        private Slide(Guid id, Colour colour, Duration duration, Transition transition, string name, bool isActive, bool isDeleted)
            : base(id)
        {
            widgets = new List<Widget>();
            Duration = duration;
            Transition = transition;
            BackgroundColour = colour;
            Name = name;
            IsActive = isActive;
            IsDeleted = isDeleted;
        }

        public static Slide Hydrate(Guid id, Colour colour, Duration duration, Transition transition, string name, bool isActive, bool isDeleted) 
        {
            return new Slide(id, colour, duration, transition, name, isActive, isDeleted);
        }

        public void ChangeBackgroundColour(Colour colour) 
        {
            this.BackgroundColour = colour;
        }

        public void AddWidget(Widget widget)
        {
            if (widget == null) 
            {
                throw new ArgumentNullException("widget", "Widget may not be null.");
            }

            widgets.Add(widget);
        }

        public void RemoveWidget(Widget widget)
        {
            if (widget == null)
            {
                throw new ArgumentNullException("widget", "Widget may not be null.");
            }

            if (!widgets.Contains(widget)) 
            {
                throw new ArgumentOutOfRangeException("widget", "Widget may not be removed as it is not contained in the Widget list.");
            }

            widgets.Remove(widget);
        }

        public void SetBackgroundColour(Colour colour) 
        {
            BackgroundColour = colour;
        }

        public void SetDuration(Duration duration)
        {
            Duration = duration;
        }

        public void SetTransition(Transition transition)
        {
            Transition = transition;
        }

        public void SetName(string name) 
        {
            Name = name;
        }

        public void ActivateSlide()
        {
            IsActive = true;
        }

        public void DeactivateSlide() 
        {
            IsActive = false;
        }

        public void DeleteSlide() 
        {
            IsDeleted = true;
        }
    }
}