using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.Enums;
using AppCampus.Domain.Models.ValueObjects;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The device response model.
    /// </summary>
    public class DeviceModel
    {
        /// <summary>
        /// The identifier (MAC address) of the device.
        /// </summary>
        public string MacAddress { get; private set; }

        /// <summary>
        /// The current device state.
        /// </summary>
        public string State { get; private set; }

        /// <summary>
        /// Last Requested Date
        /// </summary>
        public DateTime LastRequestDate { get; private set; }

        /// <summary>
        /// Last Comment associated with the device
        /// </summary>
        public string Comment { get; private set; }

        private DeviceModel()
        { }

        internal static DeviceModel From(Device device)
        {
            return DeviceModel.From(device.MacAddress, device.State, device.DeviceLastRequestDate, device.Comment);
        }

        internal static DeviceModel From(MacAddress macAddress, DeviceState state, DateTime lastRequestDate, string comment)
        {
            return new DeviceModel()
            {
                MacAddress = macAddress.ToString(),
                State = state.ToString(),
                LastRequestDate = lastRequestDate,
                Comment = comment
            };
        }
    }
}