namespace DomainAccountLock
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.txt_TempFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_OneDriveFolder = new System.Windows.Forms.Label();
            this.b_SaveSettings = new System.Windows.Forms.Button();
            this.lbl_Interval = new System.Windows.Forms.Label();
            this.txt_Interval = new System.Windows.Forms.TextBox();
            this.lbl_WindowsStartup = new System.Windows.Forms.Label();
            this.cb_LoadOnWindowsStartup = new System.Windows.Forms.CheckBox();
            this.fbd_OneDrivePath = new System.Windows.Forms.FolderBrowserDialog();
            this.b_browser = new System.Windows.Forms.Button();
            this.fbd_SymLinks = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // txt_TempFolder
            // 
            this.txt_TempFolder.Location = new System.Drawing.Point(144, 8);
            this.txt_TempFolder.Margin = new System.Windows.Forms.Padding(2);
            this.txt_TempFolder.Name = "txt_TempFolder";
            this.txt_TempFolder.ReadOnly = true;
            this.txt_TempFolder.Size = new System.Drawing.Size(287, 20);
            this.txt_TempFolder.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 1;
            // 
            // lbl_OneDriveFolder
            // 
            this.lbl_OneDriveFolder.AutoSize = true;
            this.lbl_OneDriveFolder.Location = new System.Drawing.Point(11, 11);
            this.lbl_OneDriveFolder.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_OneDriveFolder.Name = "lbl_OneDriveFolder";
            this.lbl_OneDriveFolder.Size = new System.Drawing.Size(69, 13);
            this.lbl_OneDriveFolder.TabIndex = 2;
            this.lbl_OneDriveFolder.Text = "Temp Folder:";
            // 
            // b_SaveSettings
            // 
            this.b_SaveSettings.Location = new System.Drawing.Point(498, 30);
            this.b_SaveSettings.Margin = new System.Windows.Forms.Padding(2);
            this.b_SaveSettings.Name = "b_SaveSettings";
            this.b_SaveSettings.Size = new System.Drawing.Size(56, 20);
            this.b_SaveSettings.TabIndex = 4;
            this.b_SaveSettings.Text = "Save Settings";
            this.b_SaveSettings.UseVisualStyleBackColor = true;
            this.b_SaveSettings.Click += new System.EventHandler(this.b_SaveSettings_Click);
            // 
            // lbl_Interval
            // 
            this.lbl_Interval.AutoSize = true;
            this.lbl_Interval.Location = new System.Drawing.Point(11, 34);
            this.lbl_Interval.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Interval.Name = "lbl_Interval";
            this.lbl_Interval.Size = new System.Drawing.Size(121, 13);
            this.lbl_Interval.TabIndex = 5;
            this.lbl_Interval.Text = "Check Every X Minutes:";
            // 
            // txt_Interval
            // 
            this.txt_Interval.Location = new System.Drawing.Point(144, 31);
            this.txt_Interval.Margin = new System.Windows.Forms.Padding(2);
            this.txt_Interval.Name = "txt_Interval";
            this.txt_Interval.Size = new System.Drawing.Size(40, 20);
            this.txt_Interval.TabIndex = 2;
            this.txt_Interval.Text = "0";
            this.txt_Interval.TextChanged += new System.EventHandler(this.txt_Interval_TextChanged);
            // 
            // lbl_WindowsStartup
            // 
            this.lbl_WindowsStartup.AutoSize = true;
            this.lbl_WindowsStartup.Location = new System.Drawing.Point(10, 58);
            this.lbl_WindowsStartup.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_WindowsStartup.Name = "lbl_WindowsStartup";
            this.lbl_WindowsStartup.Size = new System.Drawing.Size(130, 13);
            this.lbl_WindowsStartup.TabIndex = 7;
            this.lbl_WindowsStartup.Text = "Load on Windows Startup";
            // 
            // cb_LoadOnWindowsStartup
            // 
            this.cb_LoadOnWindowsStartup.AutoSize = true;
            this.cb_LoadOnWindowsStartup.Location = new System.Drawing.Point(144, 57);
            this.cb_LoadOnWindowsStartup.Margin = new System.Windows.Forms.Padding(2);
            this.cb_LoadOnWindowsStartup.Name = "cb_LoadOnWindowsStartup";
            this.cb_LoadOnWindowsStartup.Size = new System.Drawing.Size(15, 14);
            this.cb_LoadOnWindowsStartup.TabIndex = 3;
            this.cb_LoadOnWindowsStartup.UseVisualStyleBackColor = true;
            this.cb_LoadOnWindowsStartup.CheckedChanged += new System.EventHandler(this.cb_LoadOnWindowsStartup_CheckedChanged);
            // 
            // fbd_OneDrivePath
            // 
            this.fbd_OneDrivePath.RootFolder = System.Environment.SpecialFolder.UserProfile;
            this.fbd_OneDrivePath.ShowNewFolderButton = false;
            // 
            // b_browser
            // 
            this.b_browser.Location = new System.Drawing.Point(498, 7);
            this.b_browser.Margin = new System.Windows.Forms.Padding(2);
            this.b_browser.Name = "b_browser";
            this.b_browser.Size = new System.Drawing.Size(56, 21);
            this.b_browser.TabIndex = 1;
            this.b_browser.Text = "Browse";
            this.b_browser.UseVisualStyleBackColor = true;
            this.b_browser.Click += new System.EventHandler(this.b_browser_Click);
            // 
            // fbd_SymLinks
            // 
            this.fbd_SymLinks.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(565, 101);
            this.Controls.Add(this.b_browser);
            this.Controls.Add(this.cb_LoadOnWindowsStartup);
            this.Controls.Add(this.lbl_WindowsStartup);
            this.Controls.Add(this.lbl_Interval);
            this.Controls.Add(this.txt_Interval);
            this.Controls.Add(this.b_SaveSettings);
            this.Controls.Add(this.lbl_OneDriveFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_TempFolder);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.Text = "Domain Account Lock- Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_TempFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_OneDriveFolder;
        private System.Windows.Forms.Button b_SaveSettings;
        private System.Windows.Forms.Label lbl_Interval;
        private System.Windows.Forms.TextBox txt_Interval;
        private System.Windows.Forms.Label lbl_WindowsStartup;
        private System.Windows.Forms.CheckBox cb_LoadOnWindowsStartup;
        private System.Windows.Forms.FolderBrowserDialog fbd_OneDrivePath;
        private System.Windows.Forms.Button b_browser;
        private System.Windows.Forms.FolderBrowserDialog fbd_SymLinks;
    }
}