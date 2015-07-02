using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCampus.Tests.Aggregates
{
    [TestFixture]
    [Category("Device")]
    public class DeviceTests
    {
        public Device make_Device(MacAddress macAddress)
        {
            return Device.CreateNew(Guid.NewGuid(), macAddress, null);
        }

        [TestCase]
        public void Create_Valid_IsPending()
        {
            var device = make_Device(MacAddress.From("FF-22-33-44-55-66"));

            Assert.AreEqual(DeviceState.Pending, device.State);
            Assert.AreEqual(device.CreatedDate, device.DeviceStateChanges.First().ChangedDate);
        }
    }
}
