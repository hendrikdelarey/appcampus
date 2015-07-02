using AppCampus.Domain.Models.Enums;
using System;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class Transition : IValueObject<Transition>
    {
        public TransitionType Type { get; private set; }

        private Transition(TransitionType transitionType)
        {
            Type = transitionType;
        }

        public bool Equals(Transition other)
        {
            return (Type == other.Type);
        }

        public static Transition From(TransitionType transitionType)
        {
            return new Transition(transitionType);
        }

        public static Transition From(Transition transition)
        {
            return new Transition(transition.Type);
        }

        public static Transition From(string transition)
        {
            TransitionType transitionType;
            if (!Enum.TryParse<TransitionType>(transition, out transitionType)) 
            {
                throw new ArgumentOutOfRangeException(transition, String.Format("'{0}' is not a valid Transition type.", transition));
            }

            return new Transition(transitionType);
        }
    }
}