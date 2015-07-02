using AppCampus.Signboard.Components;
using AppCampus.Signboard.Enums;
using AppCampus.Signboard.Models;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TimetableControls;

namespace AppCampus.Signboard.DataStructures.Widgets
{
    internal class TimetableWidget : Widget
    {
        private int ERROR_THRESHHOLD = 10;

        private TimetableModel timetableModel;

        public int NumResults { get; private set; }

        public string OperatorDisplayName { get; private set; }

        public string OperatorName { get; private set; }

        public int PollingIntervalInSeconds { get; private set; }

        public string StopCode { get; private set; }

        public int WalkingTimeInSeconds { get; private set; }

        private DispatcherTimer dispatcherTimer { get; set; }

        private int ErrorCount { get; set; }

        public TimetableWidget(string operatorName, string operatorDisplayName, string stopCode, int numResults, int pollingIntervalInSeconds, int walkingTimeString, float fontFactor = 1.0f)
            : base(fontFactor)
        {
            OperatorName = operatorName;
            OperatorDisplayName = operatorDisplayName;
            StopCode = stopCode;
            NumResults = numResults;
            PollingIntervalInSeconds = pollingIntervalInSeconds;
            WalkingTimeInSeconds = walkingTimeString;
        }

        public override Widget Copy()
        {
            return new TimetableWidget(this.OperatorName, this.OperatorDisplayName, this.StopCode, this.NumResults, this.PollingIntervalInSeconds, this.WalkingTimeInSeconds, FontFactor);
        }

        public override bool Equals(Widget otherWidget)
        {
            if (Type != otherWidget.Type)
            {
                return false;
            }

            TimetableWidget other = (TimetableWidget)otherWidget;
            return StopCode == other.StopCode && PollingIntervalInSeconds == other.PollingIntervalInSeconds && OperatorName == other.OperatorName;
        }

        public override UIElement GetUiElement()
        {
            if (timetableModel == null)
            {
                return null;
            }

            var timtableControl = new PurpleHazeTimetableControl();
            timtableControl.DataContext = TimetableEntryModels.From(timetableModel, WalkingTimeInSeconds, FontFactor, OperatorDisplayName);

            return timtableControl;
        }

        public override void OnStart()
        {
            dispatcherTimer = new DispatcherTimer(DispatcherPriority.ApplicationIdle);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(PollingIntervalInSeconds);
            dispatcherTimer.Tick += OnUpdate;
            dispatcherTimer.Start();

            GetTimetable();
        }

        public void OnUpdate(object sender, EventArgs e)
        {
            GetTimetable();
        }

        private void GetTimetable()
        {
            Task signboardCoordinatorTask = Task.Factory.StartNew(() =>
            {
                var timetableComponent = new TimetableComponent();
                var result = timetableComponent.GetTimetable(OperatorName, StopCode, NumResults);

                if (result != null && result.TimetableEntry != null && result.TimetableEntry.Count > 0)
                {
                    try
                    {
                        result.TimetableEntry.Sort();

                        if (result.TimetableEntry.Count > NumResults)
                        {
                            result.TimetableEntry.RemoveRange(NumResults, result.TimetableEntry.Count - NumResults);
                        }
                    }
                    catch (Exception) { }

                    timetableModel = result;
                    State = WidgetState.Ready;
                    ErrorCount = 0;
                }
                else
                {
                    if (timetableModel == null)
                    {
                        ErrorCount++;

                        if (ErrorCount > ERROR_THRESHHOLD)
                        {
                            State = WidgetState.Error;
                        }
                    }
                    else
                    {
                        if (timetableModel.TimetableEntry == null)
                        {
                            State = WidgetState.Error;
                        }
                        else
                        {
                            for (int i = timetableModel.TimetableEntry.Count - 1; i >= 0; i--)
                            {
                                if (timetableModel.TimetableEntry[i].DepartureTime < DateTime.UtcNow)
                                {
                                    timetableModel.TimetableEntry.RemoveAt(i);
                                }
                            }

                            if (timetableModel.TimetableEntry.Count == 0)
                            {
                                State = WidgetState.Error;
                            }
                            else
                            {
                                State = WidgetState.Ready;
                            }
                        }
                    }
                }
            });
        }
    }
}