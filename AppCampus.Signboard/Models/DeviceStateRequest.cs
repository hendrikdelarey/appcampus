using System;

namespace AppCampus.Signboard.Models
{
    public class DeviceStateRequest
    {
        public DeviceState State { get; private set; }

        public DateTime ChangeDate { get; private set; }

        public string Comment { get; private set; }

        public static DeviceStateRequest From(DeviceState state, DateTime changeDate, string comment)
        {
            return new DeviceStateRequest()
            {
                State = state,
                ChangeDate = changeDate,
                Comment = comment
            };
        }
    }
}