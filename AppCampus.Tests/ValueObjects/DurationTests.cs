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
    [Category("Duration")]
    class DurationTests
    {
        public Duration make_Duration(int seconds)
        {
            return Duration.From(seconds);
        }

        [TestCase(60)]
        [TestCase(0)]
        public void Create_ValidDuration_NewDuration(int seconds)
        {
            var duration = make_Duration(seconds);

            Assert.AreEqual(duration.Seconds, seconds);
        }

        [TestCase(-60)]
        [TestCase(-1)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidDuration_ThrowException(int seconds)
        {
            make_Duration(seconds);
        }

        [TestCase]
        public void Comparison_SameDuration_IsEquals()
        {
            int seconds = 60;

            var duration1 = make_Duration(seconds);
            var duration2 = make_Duration(seconds);

            Assert.AreEqual(duration1, duration2);
            Assert.AreEqual(duration2, duration1);
        }

        [TestCase]
        public void Comparison_DifferentDuration_IsNotEquals()
        {
            var duration1 = make_Duration(15);
            var duration2 = make_Duration(60);

            Assert.AreNotEqual(duration1, duration2);
            Assert.AreNotEqual(duration2, duration1);
        }
    }
}
