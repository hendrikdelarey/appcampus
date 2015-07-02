using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Configuration;
using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.NetworkInterfacing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace AppCampus.Signboard.Windows
{
    public partial class NetworkPage : Page
    {
        public event EventHandler PageComplete;

        public MacAddress SelectedMacAddress
        {
            get
            {
                return (MacAddressComboBox.SelectedItem as NetworkInterfaceDevice).MacAddress;
            }
        }

        public Guid SelectedKey
        {
            get
            {
                return new Guid(KeyTextBlock.Text);
            }
        }

        protected bool HasValidMacAddress
        {
            get
            {
                return (MacAddressComboBox.SelectedItem as NetworkInterfaceDevice) != null;
            }
        }

        protected bool HasValidKey
        {
            get
            {
                try
                {
                    new Guid(KeyTextBlock.Text);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public NetworkPage(IEnumerable<NetworkInterfaceDevice> availableNics)
        {
            InitializeComponent();

            NextButton.IsEnabled = false;

            if (availableNics == null || !availableNics.Any())
            {
                MacAddressComboBox.IsEnabled = false;
                KeyTextBlock.IsEnabled = false;

                MessageBox.Show("There are no available network interface devices", "No Network Interface Devices", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                InitialiseFromConfig(availableNics);
            }
        }

        private void InitialiseFromConfig(IEnumerable<NetworkInterfaceDevice> availableNics)
        {
            MacAddressComboBox.ItemsSource = availableNics.ToList();
            MacAddressComboBox.DisplayMemberPath = "MacAddress";

            if (Configuration.Instance.HasKey())
            {
                KeyTextBlock.Text = Configuration.Instance.GetKey().ToString();
            }

            if (Configuration.Instance.HasMacAddress())
            {
                var configMacAddress = Configuration.Instance.GetMacAddress();

                if (availableNics.Any(x => x.MacAddress.Equals(configMacAddress)))
                {
                    MacAddressComboBox.SelectedItem = availableNics.Single(x => x.MacAddress.Equals(configMacAddress));
                }
                else
                {
                    MessageBox.Show(String.Format("The configured MAC address '{0}' is not available on this device", configMacAddress.ToString()), "Configuration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            if (HasValidMacAddress && HasValidKey)
            {
                try
                {
                    Configuration.Instance.SetKey(SelectedKey);
                    Configuration.Instance.SetMacAddress(SelectedMacAddress);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error writing to configuration file. Please check logs." + ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logger.Instance.Write("NetworkPage.NextClick", LogLevel.Critical, "Error settings configuration values", ex);
                }

                PageComplete(this, e);
            }
        }

        private void KeyTextBlockKeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            NextButton.IsEnabled = (HasValidMacAddress && HasValidKey);
        }

        private void MacAddressComboBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NextButton.IsEnabled = (HasValidMacAddress && HasValidKey);
        }
    }
}