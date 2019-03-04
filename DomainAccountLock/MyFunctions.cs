using System;
using System.Windows.Forms;
using System.IO;
using System.Timers;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Data;
using DomainAccountLock.Properties;
using System.Threading;

namespace DomainAccountLock
{
    public class MyFunctions
    {
        //User Settings Variables
        public bool UserDefinedSettingsExist;
        private string rootPath;
        private string fileName = @"\DomainAccountLock_Results.csv";

        //Timer Variables
        private System.Timers.Timer MyTimer;
        private Int32 interval;
        private Int32 timeRemaining;

        public void initApp()
        {
            initTimer();
            checkUserSettings();
            if (UserDefinedSettingsExist)
            {                
                setTimerInterval(Properties.Settings.Default.TimerInterval);
            }
        }

        #region User Settings

        public bool checkUserSettings() //Version 1.1 - Bug Fix -+
        {
            //Check if user has updated the User Settings
            if (!Properties.Settings.Default.UserDefinedSettings)
            {
                // Show Settings Form
                SettingsForm _SettingsForm = new SettingsForm();
                _SettingsForm.ShowDialog();
            }

            // Check again
            Properties.Settings.Default.Reload();
            UserDefinedSettingsExist = Properties.Settings.Default.UserDefinedSettings;
            if (!UserDefinedSettingsExist)
            {
                WrongSettings();
                return false; //Version 1.1 - Bug Fix -+
            }
            else
            {
                //Check OneDrive root Path
                rootPath = @Properties.Settings.Default.TempFolder + @"\";
                if (rootPath != null)
                {
                    if (!Directory.Exists(rootPath))
                    {
                        WrongSettings();
                        return false; //Version 1.1 - Bug Fix -+
                    }
                }

                //Check Timer
                if(Properties.Settings.Default.TimerInterval <1)
                {
                    WrongSettings();
                    return false; //Version 1.1 - Bug Fix -+
                }
            }
            return true; //Version 1.1 - Bug Fix -+
        }

        public void WrongSettings()
        {
            MessageBox.Show("Please update your settings.", "Settings Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            UpdateIconText(2);
            stopTimer();
        }

        #endregion User Settings

        #region Timer Related Functions

        public void initTimer()
        {
            MyTimer = new System.Timers.Timer();
            MyTimer.AutoReset = true;
        }

        public void setTimerInterval(Int32 newInterval)
        {
            if (newInterval <= 0)
            {
                WrongSettings();
            }
            else
            {
                interval = newInterval * 60 * 1000;
                MyTimer.Interval = interval;

                stopTimer();
                startTimer();
            }
        }

        public void startTimer()
        {
            MyTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            MyTimer.Enabled = true;

            timeRemaining = interval;
            UpdateIconText(0);
        }

        public void stopTimer()
        {
            MyTimer.Enabled = false;
            MyTimer.Elapsed -= new ElapsedEventHandler(OnTimedEvent);
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            timeRemaining -= 1 * 60 * 1000;
            if (timeRemaining <= 0)
            {
                CheckNow();
                timeRemaining = interval;
            }
            UpdateIconText(0);
        }

        private void UpdateIconText(int ProgressStatus)
        {
            switch (ProgressStatus)
            {
                case 0:
                    ProcessIcon.ni.Icon = Resources.StandardIcon;
                    ProcessIcon.ni.Text = "Domain Account Lock" + " - Next Check in: "
                    + (timeRemaining / 60 / 1000).ToString() + " minutes";
                    break;
                case 1:
                    ProcessIcon.ni.Icon = Resources.IconProgress;
                    ProcessIcon.ni.Text = "Lock Checking in progress.";
                    break;
                case 2:
                    ProcessIcon.ni.Icon = Resources.IconError;
                    ProcessIcon.ni.Text = "Timer Stopped. Check Settings.";
                    break;
                default:
                    ProcessIcon.ni.Icon = Resources.StandardIcon;
                    ProcessIcon.ni.Text = "Domain Account Lock" + " - Next Check in: "
                    + (timeRemaining / 60 / 1000).ToString() + " minutes";
                    break;
            }

        }

        #endregion Timer Related Functions

        #region Check Function

        public void CheckNow()
        {
            UpdateIconText(1);
            if (checkUserSettings())
            {
                if (File.Exists(rootPath + fileName))
                {
                    File.Delete(rootPath + fileName);
                }

                File.Create(rootPath + fileName).Close();
                Thread.Sleep(5000);
                if (File.Exists(rootPath + fileName))
                {
                    File.Delete(rootPath + fileName);
                }

                Thread.Sleep(5000);
                setTimerInterval(Properties.Settings.Default.TimerInterval);
            }
        }

        #endregion Bully Function

        #region Windows Registry Related Functions 

        public void startOnWindowsStartup(bool register)
        {
            if (register)
            {
                RegistryKey add = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                add.SetValue(@"DomainAccountLock", "\"" + Application.ExecutablePath.ToString() + "\"");
            }
            else
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    if (key != null)
                    {
                        key.DeleteValue(@"DomainAccountLock");
                    }
                }
            }
        }

        #endregion Windows Registry Related Functions 

    }
}
