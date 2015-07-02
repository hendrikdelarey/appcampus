using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;
using System;

namespace AppCampus.Tests.ValueObjects
{
    [TestFixture]
    [Category("MacAddress")]
    public class MacAddressTests
    {
        public MacAddress make_MacAddress(string address)
        {
            return MacAddress.From(address);
        }

        [TestCase(null)]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Create_NullAddress_ThrowException(string address)
        {
            make_MacAddress(address);
        }

        [TestCase("")]
        [TestCase("123")]
        [TestCase("ABC")]
        [TestCase("00-40-96-01-23-4")]
        [TestCase("00-40-96-01-23-4G")]
        [TestCase("00-40-96-01-23-4g")]
        [TestCase("001422012344")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Create_InvalidAddress_ThrowException(string address)
        {
            make_MacAddress(address);
        }

        [TestCase("00-40-96-01-23-45")]
        [TestCase("00-AB-CD-EF-23-45")]
        [TestCase("00-ab-cd-ef-23-45")]
        public void Create_ValidAddress_NewMacAddress(string address)
        {
            var macAddress = make_MacAddress(address);

            Assert.AreEqual(macAddress.Address, address);
        }

        [TestCase("")]
        [TestCase("123")]
        [TestCase("ABC")]
        [TestCase("00-40-96-01-23-4")]
        [TestCase("00-40-96-01-23-4G")]
        [TestCase("00-40-96-01-23-4g")]
        [TestCase("001422012344")]
        public void Validate_InvalidAddress_IsFalse(string address)
        {
            var isValid = MacAddress.IsValid(address);

            Assert.IsFalse(isValid);
        }

        [TestCase("00-40-96-01-23-45")]
        [TestCase("00-AB-CD-EF-23-45")]
        [TestCase("00-ab-cd-ef-23-45")]
        public void Validate_ValidAddress_IsTrue(string address)
        {
            var isValid = MacAddress.IsValid(address);

            Assert.IsTrue(isValid);
        }

        [TestCase]
        public void Comparison_SameAddress_IsEquals()
        {
            string address = "00-40-A5-01-F3-45";

            var macAddress1 = make_MacAddress(address);
            var macAddress2 = make_MacAddress(address);

            Assert.AreEqual(macAddress1, macAddress2);
            Assert.AreEqual(macAddress2, macAddress1);
        }

        [TestCase]
        public void Comparison_DifferentAddress_IsNotEquals()
        {
            var macAddress1 = make_MacAddress("00-40-A5-01-F3-45");
            var macAddress2 = make_MacAddress("01-C1-A2-51-3E-2B");

            Assert.AreNotEqual(macAddress1, macAddress2);
            Assert.AreNotEqual(macAddress2, macAddress1);
        }

        [TestCase]
        public void Comparison_NullComparison_IsNotEquals()
        {
            var macAddress = make_MacAddress("00-40-A5-01-F3-45");

            Assert.AreNotEqual(macAddress, null);
            Assert.AreNotEqual(null, macAddress);
        }
    }
}