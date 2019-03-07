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
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Reflection;
using System.Collections.Generic;

namespace DomainAccountLock
{
    public class MyFunctions
    {
        //User Settings Variables
        public bool UserDefinedSettingsExist;
        private string rootPath;
        private string fileName = "DomainAccountLock_Results.csv";

        //Timer Variables
        private System.Timers.Timer MyTimer;
        private Int32 interval;
        private Int32 timeRemaining;

        private bool fileIsOpen;
        private int ErrorCount = 0;

        public void initApp()
        {
            initTimer();
            checkUserSettings();
            if (UserDefinedSettingsExist)
            {                
                setTimerInterval(Properties.Settings.Default.TimerInterval * 60 * 1000);
            }
        }

        #region User Settings

        public bool checkUserSettings()
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
                return false; 
            }
            else
            {
                //Check Root Path
                rootPath = @Properties.Settings.Default.RootFolder + @"\";
                if (rootPath != null)
                {
                    if (!Directory.Exists(rootPath))
                    {
                        WrongSettings();
                        return false;
                    }
                }

                //Check Timer
                if(Properties.Settings.Default.TimerInterval <1)
                {
                    WrongSettings();
                    return false;
                }

                //Check UserName
                if (Properties.Settings.Default.UserName == null)
                {
                    WrongSettings();
                    return false;
                }
            }
            return true;
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
                interval = 1 * 60 * 1000; //Update every minute
                MyTimer.Interval = interval;

                stopTimer();
                startTimer(newInterval);
            }
        }

        public void startTimer(Int32 newInterval)
        {
            MyTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            MyTimer.Enabled = true;

            timeRemaining = newInterval;
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
            }
            else
            {
                UpdateIconText(0);
            }
        }

        private void UpdateIconText(int ProgressStatus)
        {
            switch (ProgressStatus)
            {
                case 0:
                    //ProcessIcon.ni.Icon = Resources.StandardIcon;
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
                case 3:
                    ProcessIcon.ni.Icon = Resources.IconError;
                    ProcessIcon.ni.Text = "Check Error. Will retry in " + (timeRemaining / 60 / 1000).ToString() + " minutes";
                    break;
                default:
                    ProcessIcon.ni.Icon = Resources.StandardIcon;
                    ProcessIcon.ni.Text = "Domain Account Lock" + " - Next Check in: "
                    + (timeRemaining / 60 / 1000).ToString() + " minutes";
                    break;
            }
        }

        private void UpdateCounter(int Count,bool calledFromCheck)
        {
            switch (Count)
            {
                case 0:
                    ProcessIcon.ni.Icon = Resources._0;
                    break;
                case 1:
                    ProcessIcon.ni.Icon = Resources._1;
                    break;
                case 2:
                    ProcessIcon.ni.Icon = Resources._2;
                    break;
                case 3:
                    ProcessIcon.ni.Icon = Resources._3;
                    break;
                case 4:
                    ProcessIcon.ni.Icon = Resources._4;
                    break;
                case 5:
                    ProcessIcon.ni.Icon = Resources._5;
                    break;
                case 6:
                    ProcessIcon.ni.Icon = Resources._6;
                    break;
                case 7:
                    ProcessIcon.ni.Icon = Resources._7;
                    break;
                case 8:
                    ProcessIcon.ni.Icon = Resources._8;
                    break;
                case 9:
                    ProcessIcon.ni.Icon = Resources._9;
                    break;
                default:
                    ProcessIcon.ni.Icon = Resources.warning;
                    break;
            }

            //Play Sound
            if ((Properties.Settings.Default.PlaySounds) && calledFromCheck)
            {
                //if (Count > 0) Console.Beep();
                if (Count > 3) Console.Beep();
                if (Count > 5) Console.Beep();
                if (Count > 7) Console.Beep();
            }
        }

        #endregion Timer Related Functions

        #region Check Function

        public void CheckNow()
        {
            if (!fileIsOpen)
            {
                UpdateIconText(1);
                if (checkUserSettings())
                {
                    if (File.Exists(rootPath + fileName))
                    {
                        File.Delete(rootPath + fileName);
                    }

                    try
                    {
                        RunPowerShell();
                    }
                    catch(Exception ex)
                    {
                        UpdateIconText(3);
                    }

                    try
                    {
                        CheckResults();
                    }
                    catch (Exception ex)
                    {
                        UpdateIconText(3);
                    }

                    Thread.Sleep(5000);
                    setTimerInterval(Properties.Settings.Default.TimerInterval * 60 * 1000);
                }
            }
        }

        private void RunPowerShell()
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace())
            {
                runspace.Open();
                PowerShell ps = PowerShell.Create();
                ps.Runspace = runspace;
                ps.AddScript(Encoding.Default.GetString(Resources.FindUserBadPwdAttempts));
                ps.Invoke();

                Command command = new Command("CheckUser", false);
                CommandParameter userParam = new CommandParameter("UserName", Settings.Default.UserName.ToString());
                CommandParameter DCParam = new CommandParameter("DCs", Settings.Default.DCList.ToString());
                CommandParameter FilePathParam = new CommandParameter("FilePath", Settings.Default.RootFolder.ToString());
                CommandParameter FileNameParam = new CommandParameter("FileName", fileName);
                command.Parameters.Add(userParam);
                command.Parameters.Add(DCParam);
                command.Parameters.Add(FilePathParam);
                command.Parameters.Add(FileNameParam);

                ps.Commands.AddCommand(command);
                ps.Invoke();
                ps.Dispose();
                runspace.Close();
                runspace.Dispose();
            }
        }

        private void CheckResults()
        {
            ErrorCount = 0;

            StreamReader reader = new StreamReader(File.OpenRead(rootPath + fileName));
            string headerLine = reader.ReadLine();
            
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                ErrorCount += Convert.ToInt16(values[7]);
                UpdateCounter(ErrorCount,true);
            }

            reader.Close();
            reader.Dispose();
        }

        public void SetFileOpen(bool open)
        {
            fileIsOpen = open;
            if(!fileIsOpen)
            {
                UpdateIconText(0);
                UpdateCounter(ErrorCount,false);
            }
            else
            {
                UpdateIconText(3);
            }
        }
        #endregion Check Function

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
