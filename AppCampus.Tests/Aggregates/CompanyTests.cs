using AppCampus.Domain.Models.Entities;
using NUnit.Framework;
using System;

namespace AppCampus.Tests.Aggregates
{
    [TestFixture]
    [Category("Company")]
    public class CompanyTests
    {
        public Company make_Company(string name)
        {
            return new Company(name);
        }

        public Company make_Company(Guid id, string name)
        {
            return Company.Hydrate(id, name);
        }

        [TestCase]
        public void Create_Valid_NewCompany()
        {
            string name = "Valid Company Name";

            var company = make_Company(name);

            Assert.IsTrue(company.Name == name);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidName_ThrowException(string name)
        {
            var company = new Company(name);
        }

        [TestCase]
        public void Comparison_SameIdentity_AreEqual()
        {
            var identity = Guid.NewGuid();
            var company1 = make_Company(identity, "Company One");
            var company2 = make_Company(identity, "Company Two");

            Assert.IsTrue(company1.Equals(company2));
            Assert.IsTrue(company2.Equals(company1));
        }

        [TestCase]
        public void Comparison_DifferentIdentity_AreNotEqual()
        {
            var company1 = make_Company(Guid.NewGuid(), "Company One");
            var company2 = make_Company(Guid.NewGuid(), "Company Two");

            Assert.IsFalse(company1.Equals(company2));
            Assert.IsFalse(company2.Equals(company1));
        }
    }
}