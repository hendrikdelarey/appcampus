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
    [Category("LayoutWeight")]
    class LayoutWeightTests
    {
        public LayoutWeight make_LayoutWeight(float weight)
        {
            return LayoutWeight.From(weight);
        }

        [TestCase(1.0f)]
        [TestCase(0.5f)]
        [TestCase(0.0002f)]
        public void Create_ValidLayoutWeight_NewLayoutWeight(float weight)
        {
            var layoutWeight = make_LayoutWeight(weight);

            Assert.AreEqual(layoutWeight.Weight, weight);
        }

        [TestCase(0.0f)]
        [TestCase(2.0f)]
        [TestCase(1.00001f)]
        [TestCase(-0.1f)]
        [TestCase(-0.91f)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidLayoutWeight_ThrowException(float weight)
        {
            make_LayoutWeight(weight);
        }

        [TestCase]
        public void Comparison_SameLayoutWeight_IsEquals()
        {
            float weight = 0.5f;

            var layoutWeight1 = make_LayoutWeight(weight);
            var layoutWeight2 = make_LayoutWeight(weight);

            Assert.AreEqual(layoutWeight1, layoutWeight2);
            Assert.AreEqual(layoutWeight2, layoutWeight1);
        }

        [TestCase]
        public void Comparison_DifferentLayoutWeight_IsNotEquals()
        {
            var layoutWeight1 = make_LayoutWeight(1.0f);
            var layoutWeight2 = make_LayoutWeight(0.5f);

            Assert.AreNotEqual(layoutWeight1, layoutWeight2);
            Assert.AreNotEqual(layoutWeight2, layoutWeight1);
        }
    }
}
