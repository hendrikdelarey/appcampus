namespace AppCampus.Signboard.Components.NetworkInterfacing
{
    public class NetworkInterfaceDevice
    {
        public string Name { get; private set; }

        public MacAddress MacAddress { get; private set; }

        public NetworkInterfaceDevice(string name, MacAddress macAddress)
        {
            Name = name;
            MacAddress = macAddress;
        }
    }
}