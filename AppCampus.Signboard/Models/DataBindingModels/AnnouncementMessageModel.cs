using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace AppCampus.Signboard.Models.DataBindingModels
{
    public class AnnouncementMessageModel : INotifyPropertyChanged
    {
        private string message;
        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                NotifyPropertyChanged("MarqueeContent");
            }
        }

        public Duration Duration = new Duration(new TimeSpan(0, 0, 10));

        public void Update(string newMessage) 
        {
            if (newMessage == null) 
            {
                return;
            }

            if (message == null || message != newMessage)
            {
                Message = newMessage;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
