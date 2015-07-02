using AppCampus.Signboard.Components.Logging;
using AppCampus.Signboard.Components.NetworkInterfacing;
using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Security;

namespace AppCampus.Signboard.Components.Configuration
{
    public class WinXpConfiguration : IRegistryEditable
    {
        private const string subkey = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Winlogon";

        private static WinXpConfiguration instance;

        public static WinXpConfiguration Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WinXpConfiguration(String.Format(@"Software\{0}", WinXpConfiguration.ProductName));
                }

                return instance;
            }
        }

        public static string ProductName
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Name;
            }
        }

        public string RegistrySubKey { get; private set; }

        private WinXpConfiguration(string registrySubKey)
        {
            RegistrySubKey = registrySubKey;
        }

        public Guid GetKey()
        {
            object registryValue = null;

            using (var key = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryValue = key.GetValue("Key");
            }

            try
            {
                return new Guid(registryValue.ToString());
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentOutOfRangeException("Key 'Key' or value does not exist in registry.", ex);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Value for key 'Key' is not in the correct format.", ex);
            }
        }

        public void SetKeyApproved() 
        {
            #if DEBUG
#else
            using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryKey.SetValue("KeyApproved", "True");
            }
#endif
        }

        public bool KeyIsApproved() 
        {
            #if DEBUG
            return true;
#else
            object registryValue = null;

            using (var key = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryValue = key.GetValue("KeyApproved");
            }

            if (registryValue == null) 
            {
                return false;
            }

            try
            {
                return registryValue.ToString() == "True";
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentOutOfRangeException("Key 'KeyApproved' or value does not exist in registry.", ex);
            }
            catch (FormatException ex)
            {
                throw new ArgumentException("Value for key 'KeyApproved' is not in the correct format.", ex);
            }
#endif
        }

        public bool HasKey()
        {
            try
            {
                GetKey();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetKey(Guid key)
        {
            #if DEBUG
#else
            if (key == null)
            {
                throw new ArgumentNullException("key", "Key cannot be null");
            }

            using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryKey.SetValue("Key", key.ToString());
            }
#endif
        }

        public void DisableScreensaver() 
        {
#if DEBUG
#else
            try
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Low, "Atempting to disable screensaver");
                string registryPath = @"Control Panel\Desktop";
                
                using (RegistryKey regKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.CurrentUser, RegistryView.Registry64).OpenSubKey(registryPath, true))
                {
                    regKey.SetValue("ScreenSaveActive", "0", RegistryValueKind.String);
                }
            }
            catch (ArgumentNullException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "ArgumentNullException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "ArgumentException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (SecurityException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "SecurityException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ObjectDisposedException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "ObjectDisposedException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "UnauthorizedAccessException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (IOException e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "IOException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Write("DisableScreensaver", LogLevel.Critical, "Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
#endif
        }

        public void TurnOffAutoUpdates() 
        {
#if DEBUG
#else
            Logger.Instance.Write("DisableScreensaver", LogLevel.Low, "Atempting to disable auto updates");
            
            try
            {
                string registryPath = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options";

                using(RegistryKey regKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(registryPath, true))
                {
                    using(var regSubKey = regKey.CreateSubKey("wupdmgr.exe"))
                    {
                        regSubKey.SetValue("Debugger", "ntsd --", RegistryValueKind.String);
                    }
                }
            }
            catch (ArgumentNullException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "ArgumentNullException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "ArgumentException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (SecurityException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "SecurityException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ObjectDisposedException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "ObjectDisposedException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "UnauthorizedAccessException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (IOException e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "IOException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Write("TurnOffAutoUpdates", LogLevel.Critical, "Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
#endif
        }

        public MacAddress GetMacAddress()
        {
            object registryValue = null;

            using (var key = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryValue = key.GetValue("MacAddress");
            }

            if (registryValue == null)
            {
                throw new ArgumentOutOfRangeException("Key 'MacAddress' or value does not exist in registry.");
            }

            var value = registryValue.ToString();

            if (!MacAddress.IsValid(value))
            {
                throw new ArgumentException("Value for key 'MacAddress' is not in the correct format.");
            }

            return MacAddress.From(value);
        }

        public bool HasMacAddress()
        {
            try
            {
                GetMacAddress();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SetMacAddress(MacAddress macAddress)
        {
            if (macAddress == null)
            {
                throw new ArgumentNullException("macAddress", "MacAddress cannot be null");
            }

            using (var registryKey = Registry.CurrentUser.CreateSubKey(RegistrySubKey))
            {
                registryKey.SetValue("MacAddress", macAddress.ToString());
            }
        }

        public bool IsInKioskMode(string exePath) 
        {
            Logger.Instance.Write("IsInKioskMode", LogLevel.Low, "Checking to see if its in kiosk mode.");

            try
            {
                RegistryKey localMachine = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
                RegistryKey regKey = localMachine.OpenSubKey(subkey, true);

                if (regKey.GetValue("Shell").ToString() != exePath) 
                {
                    Logger.Instance.Write("IsInKioskMode", LogLevel.Low, "Not in Kiosk mode");
                    return false;
                }
                Logger.Instance.Write("IsInKioskMode", LogLevel.Low, "Application already in kiosk mode");
                return true;
            }
            catch (Exception) 
            {
                Logger.Instance.Write("IsInKioskMode", LogLevel.Low, "An exception occurred when trying to see if its in kiosk mode.");
                return false;
            }
        }

        public void SetStartupKioskModeApplicationPath(string filePath) 
        {
            #if DEBUG
#else
            Logger.Instance.Write("SetAutoLogin", LogLevel.Low, "Attempting to set application in kiosk mode.");
            try
            {
                RegistryKey regKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(subkey, true);
                regKey.SetValue("Shell", filePath, RegistryValueKind.String);
                regKey.Close();
            }
            catch (ArgumentNullException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "ArgumentNullException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "ArgumentException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (SecurityException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "SecurityException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ObjectDisposedException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "ObjectDisposedException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "UnauthorizedAccessException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (IOException e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "IOException: Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Write("SetStartupKioskModeApplicationPath", LogLevel.Critical, "Uncapable of creating the regedit for kiosk mode. " + e.Message.ToString());
            }

#endif
        }

        public void SetAutoLogin(string username, string password)
        {
            #if DEBUG
#else
            Logger.Instance.Write("SetAutoLogin", LogLevel.Low, "Attempting to set auto login in registry.");
            try
            {
                RegistryKey winLogonKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\WinLogon", true);

                winLogonKey.SetValue("DefaultUserName", username, RegistryValueKind.String);
                winLogonKey.SetValue("DefaultPassword", password, RegistryValueKind.String);
                winLogonKey.SetValue("AutoAdminLogon", "1", RegistryValueKind.String);

                winLogonKey.Close();
            }
            catch (ArgumentNullException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "ArgumentNullException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "ArgumentException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (SecurityException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "SecurityException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (ObjectDisposedException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "ObjectDisposedException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "UnauthorizedAccessException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (IOException e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "IOException: Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Write("SetAutoLogin", LogLevel.Critical, "Uncapable of creating the regedit for auto login. " + e.Message.ToString());
            }
#endif
        }

        public void RemoveStartupKioskModeApplicationPath()
        {
            try
            {
                RegistryKey regKey = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(subkey, true);
                regKey.DeleteValue("Shell");
                regKey.Close();
            }
            catch (ArgumentNullException e) 
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "ArgumentNullException: Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ArgumentException e)
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "ArgumentException: Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (SecurityException e)
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "SecurityException: Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (ObjectDisposedException e)
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "ObjectDisposedException: Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (UnauthorizedAccessException e)
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "UnauthorizedAccessException: Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
            catch (Exception e)
            {
                Logger.Instance.Write("RemoveStartupKioskModeApplicationPath", LogLevel.Critical, "Uncapable of removing the regedit for kiosk mode. " + e.Message.ToString());
            }
        }
    }
}