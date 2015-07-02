using AppCampus.Domain.Models.Entities;
using AppCampus.Domain.Models.ValueObjects;
using System;

namespace AppCampus.PortalApi.Models.ResponseModels
{
    /// <summary>
    /// The signboard model.
    /// </summary>
    public class SignboardModel
    {
        /// <summary>
        /// The identifier of the signboard.
        /// </summary>
        public Guid SignboardId { get; private set; }

        /// <summary>
        /// The name of the signboard.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// The MAC address of the device which the signboard is associated.
        /// </summary>
        public string MacAddress { get; private set; }

        internal static SignboardModel From(Signboard signboard, MacAddress macAddress)
        {
            return new SignboardModel()
            {
                SignboardId = signboard.Id,
                Name = signboard.Name,
                MacAddress = macAddress.ToString()
            };
        }
    }
}