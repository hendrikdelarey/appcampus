using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;
using System.Linq;

namespace AppCampus.SignboardApi.Models.ResponseModels
{
    /// <summary>
    /// The Device State Model
    /// </summary>
    public class StateModel
    {
        /// <summary>
        /// The State of the Device
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// The Date at which the change of state occurred
        /// </summary>
        public DateTime ChangeDate { get; private set; }

        /// <summary>
        /// The Mac Address of the Device
        /// </summary>
        public string MacAddress { get; private set; }

        /// <summary>
        /// The Comment that was set on the device
        /// </summary>
        public string Comment { get; private set; }


        public static StateModel From(DeviceState state, DateTime changeDate, string macAddress, string comment)
        {
            return new StateModel()
            {
                State = state.ToString(),
                ChangeDate = changeDate,
                MacAddress = macAddress,
                Comment = comment
            };
        }

        public static StateModel From(Device device, Signboard signboard)
        {
            return new StateModel()
            {
                State = device.State.ToString(),
                ChangeDate = device.DeviceStateChanges.OrderByDescending(x => x.ChangedDate).First().ChangedDate,
                MacAddress = device.MacAddress.Address,
                Comment = device.Comment
            };
        }

        public static StateModel FromNoState()
        {
            return new StateModel()
            {
                State = "None",
                ChangeDate = DateTime.UtcNow
            };
        }
    }
}