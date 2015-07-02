using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using AppCampus.Tests.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppCampus.Tests.Aggregates
{
    [TestFixture]
    [Category("Signboard")]
    public class SignboardTests
    {
        public Signboard make_DefaultSignboard()
        {
            return new Signboard("Test Signboard", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);
        }

        public Signboard make_Signboard(string name)
        {
            return new Signboard(name, Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1);
        }

        public Signboard make_Signboard(Guid identity)
        {
            return Signboard.Hydrate(identity, "Test Signboard", Guid.NewGuid(), Guid.NewGuid(), "0.0.0", 1, DateTime.UtcNow, false, new List<ScheduledItem<ScheduledSlideshow>>(), new List<Request>());
        }

        public Signboard make_Signboard(string name, Guid companyId, Guid deviceId)
        {
            return new Signboard(name, companyId, deviceId, "0.0.0", 1);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_InvalidName_ThrowException(string name)
        {
            make_Signboard(name);
        }

        [TestCase]
        public void Create_ValidParameters_CreateNew()
        {
            var name = "Test Signboard";
            var deviceId = Guid.NewGuid();
            var companyId = Guid.NewGuid();

            var signboard = make_Signboard(name, companyId, deviceId);

            Assert.AreEqual(name, signboard.Name);
            Assert.AreEqual(deviceId, signboard.DeviceId);
            Assert.AreEqual(companyId, signboard.CompanyId);
        }

        [TestCase]
        public void Comparison_SameIdentity_AreEqual()
        {
            var identity = Guid.NewGuid();
            var company1 = make_Signboard(identity);
            var company2 = make_Signboard(identity);

            Assert.IsTrue(company1.Equals(company2));
            Assert.IsTrue(company2.Equals(company1));
        }

        [TestCase]
        public void Comparison_DifferentIdentity_AreNotEqual()
        {
            var company1 = make_Signboard(Guid.NewGuid());
            var company2 = make_Signboard(Guid.NewGuid());

            Assert.IsFalse(company1.Equals(company2));
            Assert.IsFalse(company2.Equals(company1));
        }
    }
}