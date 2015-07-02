using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Configuration;
using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.DataStructures.Slideshow;
using AppCampus.Signboard.DataStructures.Widgets;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Helpers;
using AppCampus.Signboard.Models.InputModels;
using Screensaver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace AppCampus.Signboard.Windows
{
    /// <summary>
    /// Interaction logic for SignboardWindow.xaml
    /// </summary>
    public partial class SignboardWindow : Window
    {
        private int WidgetLoadingFailCount = 0;
        private int SignboardNullFailCount = 0;
        private const int FAIL_THRESHHOLD = 10;

        private const int MinimumTimespan = 5;

        private SignboardState State { get; set; }

        private float FontFactor { get; set; }

        private bool ScreensaverToggle { get; set; }

        private SignboardCoordinator SignboardCoordinator
        {
            get;
            set;
        }

        private Slideshow CurrentSlideshow { get; set; }

        private List<Announcement> CurrentAnnouncements { get; set; }

        private DispatcherTimer UpdateStructureDispatcherTimer { get; set; }

        private DispatcherTimer TimeTickerDispatcherTimer { get; set; }

        private int CurrentSlideIndex { get; set; }

        private int Seconds { get; set; }

        private bool IsNewSlideshow { get; set; }

        private string macAddress { get; set; }

        public SignboardWindow(string macAddress)
        {
            InitializeComponent();

            string exeFilePath = Assembly.GetExecutingAssembly().Location;

#if DEBUG
#else
            if (!Configuration.Instance.IsInKioskMode(exeFilePath))
            {
                // set application in kiosk mode

                Logger.Instance.Write("OnStartup", LogLevel.Low, "Setting application to run in Kiosk mode.");
                Configuration.Instance.SetAutoLogin("Administrator", "charliepegasus85!");
                Configuration.Instance.SetStartupKioskModeApplicationPath(exeFilePath);
                Configuration.Instance.TurnOffAutoUpdates();
                Configuration.Instance.DisableScreensaver();
                HardwareComponent.Restart();
            }
#endif
            Mouse.OverrideCursor = Cursors.None;

            State = SignboardState.LoadingSlideshow;
            ScreensaverToggle = false;
            SignboardCoordinator = new SignboardCoordinator(macAddress);
            this.macAddress = macAddress;

            UpdateStructureDispatcherTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            UpdateStructureDispatcherTimer.Interval = TimeSpan.FromSeconds(SignboardCoordinator.PollingIntervalInSeconds);
            UpdateStructureDispatcherTimer.Tick += OnUpdate;
            UpdateStructureDispatcherTimer.Start();

            InitialiseSlideshow();

            Timeline.DesiredFrameRateProperty.OverrideMetadata(
                typeof(Timeline),
                new FrameworkPropertyMetadata { DefaultValue = 40 }
            );
        }

        private void InitialiseSlideshow()
        {
            CurrentSlideIndex = 0;

            TimeTickerDispatcherTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            TimeTickerDispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            TimeTickerDispatcherTimer.Tick += RenderScreen;
            TimeTickerDispatcherTimer.Start();
        }

        private void ShowLoadingSlideshow()
        {
            ResetBackgroundColorToBlack();
            HideAnnouncements();
            HideError();

            LoadingTextblock.Visibility = Visibility.Visible;
            LoadingTextblock.Text = "Initialising Slideshow...";
        }

        private void ResetBackgroundColorToBlack()
        {
            ClearSlideshowGrid();
            SolidColorBrush black = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            SlideshowGrid.Background = black;
        }

        private void ShowLoadingWidgets()
        {
            ResetBackgroundColorToBlack();
            HideAnnouncements();
            HideError();

            LoadingTextblock.Visibility = Visibility.Visible;
            LoadingTextblock.Text = "Initialising Widgets...";
        }

        private void ShowNoSlideshow()
        {
            ResetBackgroundColorToBlack();
            HideAnnouncements();
            HideError();

            LoadingTextblock.Visibility = Visibility.Visible;
            LoadingTextblock.Text = "No Slideshow attached...";
        }

        private void ShowError()
        {
            ResetBackgroundColorToBlack();
            HideAnnouncements();

            LoadingTextblock.Visibility = Visibility.Visible;
            LoadingTextblock.Text = "An error has occurred...";
        }

        private void HideError()
        {
            HideAnnouncements();
            LoadingTextblock.Visibility = Visibility.Hidden;
        }

        private void ShowScreensaver()
        {
            HideAnnouncements();
            HideLoading();
            HideError();
            ResetBackgroundColorToBlack();

            ScreensaverControl screensaver = new ScreensaverControl();
            SlideshowGrid.Children.Add(screensaver);
        }

        private void ShowSlideshow()
        {
            HideLoading();
            HideError();

            if (CurrentAnnouncements != null && CurrentAnnouncements.Count > 0)
            {
                ShowAnnouncements();
            }

            if (CurrentSlideshow == null)
            {
                State = SignboardState.Error;
                return;
            }

            Seconds++;

            if (IsMoveToNextSlide() || IsNewSlideshow)
            {
                if (!HasValidSlideToMoveTo())
                {
                    Seconds = 0;
                    ShowScreensaver();
                    return;
                }

                MoveToNextSlide();
                Seconds = 0;
            }
            else if (SlideshowGrid.Children == null || SlideshowGrid.Children.Count == 0)
            {
                ShowLoadingWidgets();
            }
        }

        private bool HasValidSlideToMoveTo()
        {
            if (CurrentSlideshow == null || CurrentSlideshow.Slides == null)
            {
                return false;
            }

            return CurrentSlideshow.Slides.Where(x => x.IsValid()).Count() > 0;
        }

        private void MoveToNextSlide()
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                //Write your Code here
                if (IsNewSlideshow)
                {
                    IsNewSlideshow = false;

                    CurrentSlideIndex = -1;
                    GoToNextSlideIndex();

                    InitialiseSlide(CurrentSlideshow.Slides[CurrentSlideIndex], TransitionType.None);
                    return;
                }

                if (CurrentSlideshow.HasOnlyOneSlide())
                {
                    InitialiseSlide(CurrentSlideshow.Slides[CurrentSlideIndex], TransitionType.None);
                    return;
                }

                GoToNextSlideIndex();

                InitialiseSlide(CurrentSlideshow.Slides[CurrentSlideIndex], TransitionType.None);
                // TODO: Transition type will be set as parameter from portal
            }, DispatcherPriority.ApplicationIdle);
        }

        public void InitialiseSlide(Slide slide, TransitionType transitionType)
        {
            if (transitionType == TransitionType.None)
            {
                slide.Render(SlideshowGrid);
                return;
            }

            // fade out
            var duration = TimeSpan.FromMilliseconds(500);
            var fadeOutAnimation = new DoubleAnimation { From = 1.0, To = 0.0, Duration = new Duration(duration) };
            var fadeOUtEasingFunction = new QuarticEase();
            fadeOUtEasingFunction.EasingMode = EasingMode.EaseInOut;
            fadeOutAnimation.EasingFunction = fadeOUtEasingFunction;

            fadeOutAnimation.Completed += (sender, e) =>
            {
                // fade in
                var fadeInAnimation = new DoubleAnimation { From = 0.0, To = 1.0, Duration = new Duration(duration) };
                var fadeInEasingFunction = new QuarticEase();
                fadeInEasingFunction.EasingMode = EasingMode.EaseInOut;
                fadeInAnimation.EasingFunction = fadeInEasingFunction;
                SlideshowGrid.BeginAnimation(UIElement.OpacityProperty, fadeInAnimation);
                slide.Render(SlideshowGrid);
            };

            SlideshowGrid.BeginAnimation(UIElement.OpacityProperty, fadeOutAnimation);
        }

        private bool IsValidState()
        {
            return (State != SignboardState.ShowingScreensaver && State != SignboardState.NoSlideshow);
        }

        private void RenderScreen(object sender, EventArgs e)
        {
            this.Dispatcher.BeginInvoke((Action)delegate()
            {
                if (State == SignboardState.LoadingSlideshow || State == SignboardState.LoadingWidgets)
                {
                    // check if loading is complete
                    if (CurrentSlideshow != null && CurrentSlideshow.HasOneOrMoreSlides())
                    {
                        State = SignboardState.ShowingSlideshow;
                    }
                }

                switch (State)
                {
                    case SignboardState.LoadingSlideshow:
                        ShowLoadingSlideshow();
                        break;

                    case SignboardState.LoadingWidgets:
                        ShowLoadingWidgets();
                        break;

                    case SignboardState.NoSlideshow:
                        //ShowNoSlideshow();
                        ShowScreensaver();
                        break;

                    case SignboardState.Error:
                        //ShowError();
                        ShowScreensaver();
                        break;

                    case SignboardState.ShowingScreensaver:
                        ShowScreensaver();
                        break;

                    case SignboardState.ShowingSlideshow:
                        ShowSlideshow();
                        break;
                }
            }, DispatcherPriority.ApplicationIdle);
        }

        // Updating on own thread
        public void OnUpdate(object sender, EventArgs e)
        {
            var signboardHealth = SignboardHealth.From(State, ScreensaverToggle, CurrentSlideshow, FontFactor);

            Task signboardCoordinatorTask = Task.Factory.StartNew(() =>
            {
                var result = SignboardCoordinator.UpdateStructure(signboardHealth);

                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() => OnSlideshowUpdate(result)),
                    DispatcherPriority.ApplicationIdle
                    );
            });
        }

        // UI thread
        public void OnSlideshowUpdate(SlideshowState slideshowState)
        {
            if (slideshowState == null)
            {
                SignboardNullFailCount++;

                if (SignboardNullFailCount >= FAIL_THRESHHOLD)
                {
                    State = SignboardState.NoSlideshow;
                }

                return;
            }
            else
            {
                SignboardNullFailCount = 0;
                if (slideshowState.IsShowScreensaver)
                {
                    State = SignboardState.ShowingScreensaver;
                    return;
                }

                State = slideshowState.SignboardState;

                FontFactor = slideshowState.FontFactor;

                if (slideshowState.NewPollingInterval >= TimeSpan.FromSeconds(MinimumTimespan))
                {
                    UpdateStructureDispatcherTimer.Interval = slideshowState.NewPollingInterval;
                }
                else
                {
                    UpdateStructureDispatcherTimer.Interval = TimeSpan.FromSeconds(MinimumTimespan);
                }

                if (slideshowState.IsNewSlideshowToStart || slideshowState.HardReload || (CurrentSlideshow == null && slideshowState.Slideshow != null))
                {
                    Logger.Instance.Write("OnSlideshowUpdate", LogLevel.Low, "Starting new or updated Slideshow.");
                    ClearSlideshowGrid();
                    IsNewSlideshow = true;
                    CurrentSlideshow = slideshowState.Slideshow.Copy();

                    CurrentSlideshow.Start();
                    return;
                }

                if (CurrentSlideshow != null && CurrentSlideshow.Slides != null)
                {
                    foreach (Slide slide in CurrentSlideshow.Slides)
                    {
                        foreach (Widget widget in slide.Widgets)
                        {
                            widget.UpdateFontFactor(slideshowState.FontFactor);
                        }
                    }
                }

                if (State == SignboardState.NoSlideshow)
                {
                    ClearSlideshowGrid();
                    ShowScreensaver();
                    CurrentSlideshow = null;
                    return;
                }

                if (CurrentSlideshow == null)
                {
                    Logger.Instance.Write("OnSlideshowUpdate", LogLevel.Low, "There is no current slideshow.");
                    return;
                }

                // check if all the widgets in all the slides are loaded
                if (CurrentSlideshow.HasOneOrMoreSlides())
                {
                    WidgetLoadingFailCount = 0;

                    // Check if announcements have changed
                    if (slideshowState.Announcements != null && CurrentAnnouncements == null)
                    {
                        CurrentAnnouncements = slideshowState.Announcements;
                        RenderAnnouncements(slideshowState.FontFactor);
                    }
                    else if (CurrentAnnouncements != null && (slideshowState.Announcements == null || slideshowState.Announcements.Count == 0))
                    {
                        CurrentAnnouncements = slideshowState.Announcements;
                        RenderAnnouncements(slideshowState.FontFactor);
                    }
                    else if (slideshowState.Announcements != null && CurrentAnnouncements != null && !CurrentAnnouncements.SequenceEqual(slideshowState.Announcements))
                    {
                        CurrentAnnouncements = slideshowState.Announcements;
                        RenderAnnouncements(slideshowState.FontFactor);
                    }

                    State = SignboardState.ShowingSlideshow;
                }
                else
                {
                    State = SignboardState.LoadingWidgets;
                    WidgetLoadingFailCount++;

                    if (WidgetLoadingFailCount >= FAIL_THRESHHOLD)
                    {
                        Logger.Instance.Write("OnSlideshowUpdate", LogLevel.Medium, "Failing to load all widgets.");
                        State = SignboardState.Error;
                    }
                }
            }
        }

        // UI thread
        private void RenderAnnouncements(float fontSizeFacotr)
        {
            if (CurrentAnnouncements == null || CurrentAnnouncements.Count == 0)
            {
                HideAnnouncements();
                return;
            }

            AnnouncementRow.Height = new GridLength(1, GridUnitType.Star);
            AnnouncementTextBlock.Inlines.Clear();

            if (fontSizeFacotr < 0)
            {
                fontSizeFacotr = 1;
            }

            AnnouncementTextBlock.FontSize = (int)(40 * fontSizeFacotr);

            foreach (Announcement announcement in CurrentAnnouncements)
            {
                if (announcement.Severity == AnnouncementSeverity.Critical)
                {
                    AnnouncementTextBlock.Inlines.Add(new Run(announcement.Message + "\t") { Foreground = System.Windows.Media.Brushes.Red });
                }
                else
                {
                    AnnouncementTextBlock.Inlines.Add(new Run(announcement.Message + "\t") { Foreground = System.Windows.Media.Brushes.White });
                }
            }

            double distance = SystemParameters.PrimaryScreenWidth + FontHelper.MeasureString(AnnouncementTextBlock.Text, AnnouncementTextBlock).Width;
            double pixelsPerSecond = 100;
            // TODO: add a parameter that the user can set via portal to control speed of announcements

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = this.ActualWidth;
            doubleAnimation.To = -FontHelper.MeasureString(AnnouncementTextBlock.Text, AnnouncementTextBlock).Width;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = TimeSpan.FromSeconds(distance / pixelsPerSecond);
            AnnouncementTextBlock.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }

        private void GoToNextSlideIndex()
        {
            CurrentSlideIndex++;

            if (CurrentSlideIndex >= CurrentSlideshow.Slides.Count)
            {
                CurrentSlideIndex = 0;
            }

            if (!CurrentSlideshow.Slides[CurrentSlideIndex].IsValid())
            {
                GoToNextSlideIndex();
            }
        }

        private bool IsCurrentSlideshowValid()
        {
            if (CurrentSlideshow == null || CurrentSlideshow.Slides == null || CurrentSlideshow.Slides.Count == 0)
            {
                return false;
            }

            if (!CurrentSlideshow.HasOneOrMoreSlides())
            {
                return false;
            }

            return true;
        }

        private bool IsNewSlideshowToDisplay(Slideshow newSlideshow)
        {
            if (newSlideshow == null || newSlideshow.Slides == null || newSlideshow.Slides.Count == 0)
            {
                return false;
            }

            if (CurrentSlideshow == null)
            {
                return true;
            }

            return !CurrentSlideshow.Equals(newSlideshow);
        }

        private Slideshow GetSlideshow(Structure structure)
        {
            if (structure == null)
            {
                return null;
            }

            return structure.Slideshow;
        }

        private bool IsMoveToNextSlide()
        {
            return IsCurrentSlideIndexOutOfBounds() || Seconds >= CurrentSlideshow.Slides[CurrentSlideIndex].DurationInSeconds;
        }

        private bool IsCurrentSlideIndexOutOfBounds()
        {
            if (CurrentSlideshow == null || CurrentSlideshow.Slides == null || CurrentSlideshow.Slides.Count == 0)
            {
                return false;
            }

            return CurrentSlideIndex > CurrentSlideshow.Slides.Count - 1;
        }

        private void HideLoading()
        {
            LoadingTextblock.Visibility = Visibility.Collapsed;
            LoadingTextblock.Text = "";
        }

        private void HideAnnouncements()
        {
            AnnouncementTextBlock.Visibility = Visibility.Collapsed;
            AnnouncementRow.Height = new GridLength(0, GridUnitType.Star);
        }

        private void ShowAnnouncements()
        {
            AnnouncementTextBlock.Visibility = Visibility.Visible;
            AnnouncementRow.Height = new GridLength(1, GridUnitType.Star);
        }

        private void ClearSlideshowGrid()
        {
            SlideshowGrid.Children.Clear();
            SlideshowGrid.RowDefinitions.Clear();
            SlideshowGrid.ColumnDefinitions.Clear();
        }
    }
}