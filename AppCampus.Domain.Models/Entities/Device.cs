using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.Entities
{
    public class Device : DomainEntity<Guid>, IAggregateRoot
    {
        private MacAddress macAddress;

        private List<DeviceStateChange> deviceStateChanges;

        public Guid CompanyId { get; private set; }

        public string Comment { get; set; }

        public MacAddress MacAddress
        {
            get
            {
                return macAddress;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "MAC address cannot be null");
                }

                macAddress = value;
            }
        }

        public IReadOnlyCollection<DeviceStateChange> DeviceStateChanges
        {
            get
            {
                return deviceStateChanges.OrderByDescending(x => x.ChangedDate).ToList().AsReadOnly();
            }
        }

        public DeviceState State
        {
            get
            {
                return deviceStateChanges.OrderByDescending(x => x.ChangedDate).First().State;
            }
        }

        public DateTime DeviceLastRequestDate
        {
            get
            {
                return deviceStateChanges.OrderByDescending(x => x.ChangedDate).First().ChangedDate;
            }
        }

        public DateTime CreatedDate { get; private set; }

        // Create
        private Device(Guid companyId, MacAddress macAddress, string comment)
            : base(CombIdentityFactory.GenerateIdentity())
        {
            CompanyId = companyId;
            MacAddress = macAddress;
            CreatedDate = DateTime.UtcNow;
            Comment = comment;
            deviceStateChanges = new List<DeviceStateChange>() { new DeviceStateChange(DeviceState.Pending, CreatedDate) };
        }

        // Hydrate
        private Device(Guid id, Guid companyId, MacAddress macAddress, string comment, ICollection<DeviceStateChange> stateChanges, DateTime createdDate)
            : base(id)
        {
            CompanyId = companyId;
            MacAddress = macAddress;
            CreatedDate = createdDate;
            Comment = comment;
            deviceStateChanges = stateChanges.ToList();
        }

        public void Approve()
        {
            deviceStateChanges.Add(new DeviceStateChange(DeviceState.Approved, DateTime.UtcNow));
        }

        public void Pend()
        {
            if (State == DeviceState.Approved)
            {
                throw new InvalidOperationException("Approved device cannot be pending");
            }

            if (State == DeviceState.Blocked)
            {
                throw new InvalidOperationException("Blocked device cannot be pending");
            }

            deviceStateChanges.Add(new DeviceStateChange(DeviceState.Pending, DateTime.UtcNow));
        }

        public void Decline()
        {
            if (State == DeviceState.Approved)
            {
                throw new InvalidOperationException("Approved device cannot be declined");
            }

            deviceStateChanges.Add(new DeviceStateChange(DeviceState.Declined, DateTime.UtcNow));
        }

        public void Block()
        {
            if (State == DeviceState.Approved)
            {
                throw new InvalidOperationException("Approved device cannot be blocked");
            }

            deviceStateChanges.Add(new DeviceStateChange(DeviceState.Blocked, DateTime.UtcNow));
        }

        public static Device CreateNew(Guid companyId, MacAddress macAddress, string comment)
        {
            return new Device(companyId, macAddress, comment);
        }

        public static Device Hydrate(Guid id, Guid companyId, MacAddress macAddress, string comment, ICollection<DeviceStateChange> deviceStateChanges, DateTime createdDate)
        {
            return new Device(id, companyId, macAddress, comment, deviceStateChanges, createdDate);
        }
    }
}