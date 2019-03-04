using System;
using System.Data;
using System.Windows.Forms;
using System.IO;

namespace DomainAccountLock
{
    public partial class SettingsForm : Form
    {
        bool startWithWindowsChanged = false;
        bool isDirty = false;
        DataTable SymLinksTable = new DataTable();

        public SettingsForm()
        {
            InitializeComponent();

            // Load user settings           
            if (ProcessIcon.fn.UserDefinedSettingsExist)
            {
                loadSettings();
            }
        }

        #region Settings Handling

        private void loadSettings()
        {
            // Load user settings and update form controls
            Properties.Settings.Default.Reload();
            txt_TempFolder.Text = Properties.Settings.Default.TempFolder;
            txt_Interval.Text = Properties.Settings.Default.TimerInterval.ToString();
            cb_LoadOnWindowsStartup.Checked = Properties.Settings.Default.LoadOnWindowsStartup;
            isDirty = false;
        }

        private bool validateSettings()
        {
            // Check OneDrive Root Folder exists
            if (txt_TempFolder.Text == "")
            {
                MessageBox.Show("Temp folder must have a value", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (!Directory.Exists(txt_TempFolder.Text + @"\"))
            {
                MessageBox.Show("Temp folder not found.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Check Interval
            if (txt_Interval.Text == "")
            {
                MessageBox.Show("Minutes must be more than 0.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int intervalInt = 0;
            if (int.TryParse(txt_Interval.Text, out intervalInt))
            {
                if (intervalInt <= 0)
                {
                    MessageBox.Show("Minutes must be more than 0.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Value : " + txt_Interval.Text + " is not a number.", "Value Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void saveSettings()
        {           
            // Save user settings
            Properties.Settings.Default.TempFolder = txt_TempFolder.Text;
            Properties.Settings.Default.TimerInterval = Convert.ToInt32(txt_Interval.Text);
            Properties.Settings.Default.LoadOnWindowsStartup = cb_LoadOnWindowsStartup.Checked;
            Properties.Settings.Default.UserDefinedSettings = true;
            Properties.Settings.Default.Save();
            isDirty = false;

            // Start Timer
            ProcessIcon.fn.setTimerInterval(Properties.Settings.Default.TimerInterval);

            if (startWithWindowsChanged)
            {
                //Update Windows Registry Key (Add/Remove)
                ProcessIcon.fn.startOnWindowsStartup(Properties.Settings.Default.LoadOnWindowsStartup);
                startWithWindowsChanged = false;
            }

            MessageBox.Show("Settings saved successfully.","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);            
        }

        #endregion Settings Handling

        #region Form Controls

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isDirty)
            {
                if (MessageBox.Show("You have not saved your changes. Do you want to close the form?", "Unsaved settings",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void b_SaveSettings_Click(object sender, EventArgs e)
        {
            if (validateSettings())
            {
                saveSettings();
                loadSettings();
            }
        }

        private void b_browser_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd_OneDrivePath = new FolderBrowserDialog();
            if (fbd_OneDrivePath.ShowDialog() != System.Windows.Forms.DialogResult.Cancel)
            {
                if (fbd_OneDrivePath.SelectedPath != null)
                {
                    txt_TempFolder.Text = fbd_OneDrivePath.SelectedPath;
                }                
            }
            isDirty = true;
        }

        private void txt_Interval_TextChanged(object sender, EventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(txt_Interval.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter numbers only.");
                txt_Interval.Text = "";
            }
            isDirty = true;
        }

        private void cb_LoadOnWindowsStartup_CheckedChanged(object sender, EventArgs e)
        {
            startWithWindowsChanged = true;
            isDirty = true;
        }

        #endregion Form Controls

        private void SettingsForm_Load(object sender, EventArgs e)
        {

        }
    }
}
