using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows;

namespace AppCampus.Signboard.Components.Logging
{
    public enum LogLevel
    {
        Critical, 
        Medium,
        Low
    }

    public class Logger
    {
        private readonly string loggingDirectory;

        private static Logger instance;

        public static Logger Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new Logger();
                }

                return instance;
            }
        }

        private Logger()
        {
            loggingDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), Assembly.GetEntryAssembly().GetName().Name);
            loggingDirectory = Path.Combine(loggingDirectory, "logs");

            Directory.CreateDirectory(loggingDirectory);
        }

        public void Write(string context, LogLevel level, string message, Exception exception)
        {
            var logItem = new LogItem()
            {
                Date = DateTime.Now,
                Context = context,
                Level = level,
                Message = message,
                Exception = exception
            };

            try
            {
                var filePath = Path.Combine(loggingDirectory, String.Format("{0}.log", DateTime.Now.ToString("yyyy-MM-dd")));

                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(logItem.ToString());
                }
            }
            catch (Exception)
            { }
        }

        public void Write(string context, LogLevel level, string message)
        {
            Write(context, level, message, null);
        }
    }

    [Serializable]
    public class LogItem
    {
        public DateTime Date { get; set; }

        public string Context { get; set; }

        public LogLevel Level { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public override string ToString()
        {
            return String.Format("{0}\t{1}\t{2}\t{3}\t{4}", Date, Context, Level, Message, Exception);
        }
    }
}
