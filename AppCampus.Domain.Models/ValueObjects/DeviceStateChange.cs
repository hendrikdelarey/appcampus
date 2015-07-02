using AppCampus.Domain.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drumble.DomainDrivenArchitecture.Domain.Models;

namespace AppCampus.Domain.Models.ValueObjects
{
    public class DeviceStateChange : IValueObject<DeviceStateChange>
    {
        public DeviceState State { get; private set; }

        public DateTime ChangedDate { get; private set; }

        public DeviceStateChange(DeviceState state, DateTime changedDate)
        {
            State = state;
            ChangedDate = changedDate;
        }


        public bool Equals(DeviceStateChange other)
        {
            if (other == null)
            {
                return false;
            }

            return State.Equals(other.State) && ChangedDate.Equals(other.ChangedDate);
        }

        public override string ToString()
        {
            return State.ToString();
        }
    }
}
