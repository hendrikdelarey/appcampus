using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.NetworkInterfacing;
using AppCampus.Signboard.Components.SignboardApi;
using AppCampus.Signboard.Models;
using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace AppCampus.Signboard.Windows
{
    public partial class SignboardConfigPage : Page
    {
        private bool isRetrievingState;

        private DeviceStateRequest state;

        public DeviceStateRequest State
        {
            get
            {
                return state;
            }
            private set
            {
                state = value;
                FormStateChanged();
            }
        }

        public Guid Key { get; private set; }

        public string Comment { get; private set; }

        public MacAddress MacAddress { get; private set; }

        public event EventHandler ChangeNetworkDetails;

        public SignboardConfigPage(MacAddress macAddress, Guid key)
        {
            InitializeComponent();

            MacAddress = macAddress;
            Key = key;
            isRetrievingState = false;

            MacAddressTextBlock.Text = MacAddress.Address;
            KeyTextBlock.Text = Key.ToString();

            RefreshState();
        }

        public void RefreshState()
        {
            if (!isRetrievingState)
            {
                isRetrievingState = true;
                RefreshGifImage.StartAnimate();

                DeviceStateTextBlock.Text = "Retrieving state...";
                CommentValueTextBox.IsEnabled = false;
                DeviceStateTextBlock.Foreground = Brushes.Black;

                var apiComponent = new ApiComponent(new Uri(ConfigurationManager.AppSettings["SignboardApiUri"]));

                var task = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        var stateRequest = apiComponent.GetState(MacAddress);

                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            State = stateRequest;
                        }));
                    }
                    catch(Exception)
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            MessageBox.Show("An error occurred. Please check the logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            DeviceStateTextBlock.Text = "(Operation Failed)";
                            RetryButton.Visibility = Visibility.Visible;
                            RequestButton.Visibility = Visibility.Collapsed;
                        }));
                    }
                    finally
                    {
                        this.Dispatcher.Invoke((Action)(() =>
                        {
                            RefreshGifImage.StopAnimate();
                        }));

                        isRetrievingState = false;
                    }
                });

            }
        }

        public void FormStateChanged()
        {
            DeviceStateTextBlock.Text = State.State.ToString();
            DeviceStateDateTextBlock.Text = String.Format("({0} {1})", State.ChangeDate.ToShortDateString(), State.ChangeDate.ToShortTimeString());

            CommentValueTextBox.IsEnabled = true;
            switch (State.State)
            {
                case DeviceState.None:
                    DeviceStateTextBlock.Foreground = Brushes.Black;
                    break;

                case DeviceState.Pending:
                    DeviceStateTextBlock.Foreground = Brushes.Orange;
                    break;

                case DeviceState.Approved:
                    CommentValueTextBox.IsEnabled = false;
                    DeviceStateTextBlock.Foreground = Brushes.Green;
                    Components.Configuration.Configuration.Instance.SetKeyApproved();
                    var slideshowWindow = new SignboardWindow(Components.Configuration.Configuration.Instance.GetMacAddress().Address);
                    slideshowWindow.ShowDialog();
                    break;

                case DeviceState.Declined:
                    DeviceStateTextBlock.Foreground = Brushes.Red;
                    break;

                case DeviceState.Blocked:
                    DeviceStateTextBlock.Foreground = Brushes.Red;
                    break;
            }

            RetryButton.Visibility = Visibility.Collapsed;
            RequestButton.IsEnabled = (State.State == DeviceState.None) || (State.State == DeviceState.Declined);
            RequestButton.Visibility = (State.State == DeviceState.None) || (State.State == DeviceState.Declined) || (State.State == DeviceState.Blocked) || (State.State == DeviceState.Pending) ? Visibility.Visible : Visibility.Collapsed;
            RefreshGifImage.Visibility = (State.State == DeviceState.Pending) ? Visibility.Visible : Visibility.Collapsed;
            KeyValueTextBlock.Visibility = (State.State == DeviceState.None) ? Visibility.Visible : Visibility.Collapsed;
            MacAddressValueTextBlock.Visibility = (State.State == DeviceState.None) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void RequestClick(object sender, RoutedEventArgs e)
        {
            RequestButton.Content = "Sending...";
            RequestButton.IsEnabled = false;

            Comment = CommentValueTextBox.Text;
            var apiComponent = new ApiComponent(new Uri(ConfigurationManager.AppSettings["SignboardApiUri"]));

            Task.Factory.StartNew(() =>
            {
                try
                {
                    var stateRequest = apiComponent.RequestApproval(Key, MacAddress, Comment);

                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        State = stateRequest;
                    }));
                }
                catch (Exception)
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        MessageBox.Show("An error occurred. Please check the logs.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }));
                }
                finally
                {
                    this.Dispatcher.Invoke((Action)(() =>
                    {
                        RefreshGifImage.StopAnimate();
                        RequestButton.Content = "Request";
                        RequestButton.IsEnabled = true;
                    }));

                    isRetrievingState = false;
                }
            });
        }

        private void RetryClick(object sender, RoutedEventArgs e)
        {
            RetryButton.Visibility = Visibility.Collapsed;
            RequestButton.Visibility = Visibility.Visible;
            RefreshState();
        }
         
        private void ChangeMacAddressHyperlinkClick(object sender, RoutedEventArgs e)
        {
            ChangeNetworkDetails(this, e);
        }

        private void ChangeKeyHyperlinkClick(object sender, RoutedEventArgs e)
        {
            ChangeNetworkDetails(this, e);
        }

        private void RefreshGifImageMouseDown(object sender, MouseButtonEventArgs e)
        {
            DeviceStateTextBlock.Text = String.Empty;
            DeviceStateDateTextBlock.Text = String.Empty;
            RefreshState();
        }

        private void MacAddressCopyMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(MacAddress.ToString());
        }

        private void KeyCopyMouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(Key.ToString());
        }
    }
}