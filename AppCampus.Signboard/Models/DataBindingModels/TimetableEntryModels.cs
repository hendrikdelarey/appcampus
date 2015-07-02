using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Timers;

namespace AppCampus.Signboard.Models
{
    public class TimetableEntryModels : INotifyPropertyChanged
    {
        private TimetableEntryModels(TimetableModel model, int walkingTimeInSeconds, float fontFactor, string operatorDisplayName)
        {
            OperatorName = operatorDisplayName;
            TimeDisplayString = DateTime.Now.ToString("HH:mm");
            DateDisplayString = DateTime.Now.ToString("dd/MM/yyyy");
            StopName = model.StopName;
            walkingTimeValue = walkingTimeInSeconds;
            WalkingTime = walkingTimeInSeconds/60 == 0 ? "" : String.Format("{0}min walking to stop", walkingTimeInSeconds/60);

            DateHeaderFontSize = (int) (75 * fontFactor);
            TimeHeaderFontSize = (int)(135 * fontFactor);
            OperatorHeaderFontSize = (int)(75 * fontFactor);
            StopNameFontSize = (int)(60 * fontFactor);
            TimetableFontSize = (int)(50 * fontFactor);

            TimetableEntryModelCollection = model.TimetableEntry.Select(x =>
                new TimetableEntryModel()
                {
                    RouteName = x.RouteName,
                    Destination = x.Destination,
                    DepartureTime = x.DepartureTime
                }).ToList();

            Timer timer = new Timer();
            timer.Interval = 5000; // 5 second updates
            timer.Elapsed += timerElapsed;
            timer.Start();
        }

        private int dateHeaderFontSize;
        public int DateHeaderFontSize 
        { 
            get 
            {
                return dateHeaderFontSize;
            } 
            set 
            {
                dateHeaderFontSize = value;
                NotifyPropertyChanged("DateHeaderFontSize");
            } 
        }

        private int timeHeaderFontSize;
        public int TimeHeaderFontSize
        {
            get
            {
                return timeHeaderFontSize;
            }
            set
            {
                timeHeaderFontSize = value;
                NotifyPropertyChanged("TimeHeaderFontSize");
            }
        }

        private int operatorHeaderFontSize;
        public int OperatorHeaderFontSize
        {
            get
            {
                return operatorHeaderFontSize;
            }
            set
            {
                operatorHeaderFontSize = value;
                NotifyPropertyChanged("OperatorHeaderFontSize");
            }
        }

        private int stopNameFontSize;
        public int StopNameFontSize
        {
            get
            {
                return stopNameFontSize;
            }
            set
            {
                stopNameFontSize = value;
                NotifyPropertyChanged("StopNameFontSize");
            }
        }

        private int timetableFontSize;
        public int TimetableFontSize
        {
            get
            {
                return timetableFontSize;
            }
            set
            {
                timetableFontSize = value;
                NotifyPropertyChanged("TimetableFontSize");
            }
        }

        private string dateDisplayString;
        public string DateDisplayString
        {
            get
            {
                return dateDisplayString;
            }
            set
            {
                dateDisplayString = value;
                NotifyPropertyChanged("DateDisplayString");
            }
        }

        private string operatorName;
        public string OperatorName
        {
            get
            {
                return operatorName;
            }
            set
            {
                operatorName = value;
                NotifyPropertyChanged("OperatorName");
            }
        }

        private int walkingTimeValue;

        private string walkingTime;
        public string WalkingTime
        {
            get
            {
                return walkingTime;
            }
            set
            {
                walkingTime = value;
                NotifyPropertyChanged("WalkingTime");
            }
        }

        private string stopName;
        public string StopName
        {
            get
            {
                return stopName;
            }
            set
            {
                stopName = value;
                NotifyPropertyChanged("StopName");
            }
        }

        private string timeDisplayString;
        public string TimeDisplayString
        {
            get
            {
                return timeDisplayString;
            }
            set
            {
                timeDisplayString = value;
                NotifyPropertyChanged("TimeDisplayString");
            }
        }

        private List<TimetableEntryModel> timetableEntryModelCollection;
        public List<TimetableEntryModel> TimetableEntryModelCollection
        {
            get
            {
                return timetableEntryModelCollection;
            }
            set
            {
                timetableEntryModelCollection = value;
                NotifyPropertyChanged("TimetableEntryModelCollection");
            }
        }

        void timerElapsed(object sender, ElapsedEventArgs e)
        {
            TimeDisplayString = DateTime.Now.ToString("HH:mm");
            DateDisplayString = DateTime.Now.ToString("dd/MM/yyyy");
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public static TimetableEntryModels From(TimetableModel model, int walkingTimeInSeconds, float fontFactor, string operatorDisplayName)
        {
            return new TimetableEntryModels(model, walkingTimeInSeconds, fontFactor, operatorDisplayName);
        }
    }
}
