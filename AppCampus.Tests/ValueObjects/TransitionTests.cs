using System;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;

namespace AppCampus.Tests.ValueObjects
{
    [TestFixture]
    [Category("Transition")]
    public class TransitionTests
    {
        public Transition make_Transition(TransitionType transitionType)
        {
            return Transition.From(transitionType);
        }

        [TestCase(TransitionType.None)]
        public void Create_ValidTransition_NewTransition(TransitionType transitionType)
        {
            var transition = make_Transition(transitionType);

            Assert.AreEqual(transition.Type, transitionType);
        }

        [TestCase]
        public void Comparison_SameTransition_IsEquals()
        {
            TransitionType transitionType = TransitionType.None;

            var transition1 = make_Transition(transitionType);
            var transition2 = make_Transition(transitionType);

            Assert.AreEqual(transition1, transition2);
            Assert.AreEqual(transition2, transition1);
        }
    }
}
