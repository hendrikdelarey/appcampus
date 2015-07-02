using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;

namespace AppCampus.Signboard.Components.NetworkInterfacing
{
    public class NetworkComponent
    {
        public static ICollection<NetworkInterfaceDevice> NetworkInterfaces()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(x => x.OperationalStatus == OperationalStatus.Up)
                .Where(x => MacAddress.IsValid(FormatPhysicalAddress(x.GetPhysicalAddress().GetAddressBytes())))
                .Select(x => new NetworkInterfaceDevice(x.Name, MacAddress.From(FormatPhysicalAddress(x.GetPhysicalAddress().GetAddressBytes()))))
                .ToList();
        }

        private static string FormatPhysicalAddress(byte[] address)
        {
            return String.Join("-", address.Select(b => (string)b.ToString("X2")).ToArray());
        }
    }
}