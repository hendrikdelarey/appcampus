using AppCampus.Signboard.Components;
using AppCampus.Signboard.Components.Configuration;
using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Models;
using AppCampus.Signboard.Windows;
using System;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Threading;

namespace AppCampus.Signboard
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Logger.Instance.Write("OnStartup", LogLevel.Low, "Application Started.");

            AppDomain.CurrentDomain.UnhandledException += AppDispatcherUnhandledException;

            var configurationWindow = new ConfigurationWindow();
            configurationWindow.ShowDialog();
        }

        static void AppDispatcherUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Instance.Write("AppDispatcherUnhandledException", LogLevel.Critical, "An unhandled exception was thrown. " + e.ToString());
        }
    }
}
