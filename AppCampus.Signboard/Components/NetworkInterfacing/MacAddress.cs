using System;
using System.Text.RegularExpressions;

namespace AppCampus.Signboard.Components.NetworkInterfacing
{
    public class MacAddress
    {
        public string Address { get; private set; }

        /// <summary>
        /// Only accepts a MAC address string in format XX-XX-XX-XX-XX-XX
        /// </summary>
        private MacAddress(string macAddress)
        {
            if (macAddress == null)
            {
                throw new ArgumentNullException("macAddress");
            }

            macAddress = macAddress.Trim();

            if (MacAddress.IsValid(macAddress))
            {
                Address = macAddress;
            }
            else
            {
                throw new ArgumentOutOfRangeException("macAddress", String.Format("Supplied address '{0}' is not a valid MAC address", macAddress));
            }
        }

        public static bool IsValid(string macAddress)
        {
            return macAddress != null && Regex.IsMatch(macAddress, "^[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}-[0-9A-F]{2}$", RegexOptions.IgnoreCase);
        }

        public static MacAddress From(string macAddress)
        {
            return new MacAddress(macAddress);
        }

        public bool Equals(MacAddress other)
        {
            if (other == null)
            {
                return false;
            }

            return Address.Equals(other.Address, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Address;
        }
    }
}