using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Configuration;
using AppCampus.Signboard.Components.NetworkInterfacing;
using System;
using System.Windows;

namespace AppCampus.Signboard.Windows
{
    public partial class ConfigurationWindow : Window
    {
        public NetworkPage NetworkPage { get; private set; }

        public SignboardConfigPage SignboardPage { get; private set; }

        public ConfigurationWindow()
        {
            InitializeComponent();

            if (Configuration.Instance.HasMacAddress() && Configuration.Instance.HasKey() && Configuration.Instance.KeyIsApproved())
            {
                var slideshowWindow = new SignboardWindow(Configuration.Instance.GetMacAddress().Address);
                slideshowWindow.ShowDialog();
            }
            else
            {
                NetworkPage = new NetworkPage(NetworkComponent.NetworkInterfaces());
                NetworkPage.PageComplete += NetworkPageComplete;

                Content = NetworkPage;
            }
        }

        private void NetworkPageComplete(object sender, EventArgs e)
        {
            SignboardPage = new SignboardConfigPage(NetworkPage.SelectedMacAddress, NetworkPage.SelectedKey);
            SignboardPage.ChangeNetworkDetails += SignboardPageChangeNetworkDetails;

            Content = SignboardPage;
        }

        private void SignboardPageChangeNetworkDetails(object sender, EventArgs e)
        {
            if (NetworkPage == null)
            {
                NetworkPage = new NetworkPage(NetworkComponent.NetworkInterfaces());
                NetworkPage.PageComplete += NetworkPageComplete;
            }

            Content = NetworkPage;
        }
    }
}