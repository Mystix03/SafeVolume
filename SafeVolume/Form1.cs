using System;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;
using Microsoft.Win32;




namespace SafeVolume
{
    public partial class Form1 : Form, IMMNotificationClient
    {
        private MMDeviceEnumerator enumerator;
        private float volumeCap = 0.3f;
        private bool isEnabled = false;
        private bool allowExit = false;

        public Form1()
        {
            InitializeComponent();
            // Load saved settings
            volumeCap = Properties.Settings.Default.VolumeCap;
            isEnabled = Properties.Settings.Default.IsEnabled;

            // Apply to UI
            trackBarVolume.Value = (int)(volumeCap * 100);
            checkBoxEnable.Checked = isEnabled;

            labelVolume.Text = $"Volume Cap: {trackBarVolume.Value}%";

            // Sync startup checkbox with registry
            string appName = "SafeVolume";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", false);

            if (key != null && key.GetValue(appName) != null)
            {
                checkBoxStartup.Checked = true;
                key.Close();
            }

            enumerator = new MMDeviceEnumerator();
            enumerator.RegisterEndpointNotificationCallback(this);

            
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            this.Hide();
            this.Text = "SafeVolume";
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (!allowExit)
            {
                e.Cancel = true;   // block normal close
                this.Hide();       // hide instead
            }
        }




        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            if (flow == DataFlow.Render && isEnabled)
            {
                var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                device.AudioEndpointVolume.MasterVolumeLevelScalar = volumeCap;

                //  SHOW NOTIFICATION
                notifyIcon1.BalloonTipTitle = "SafeVolume";
                notifyIcon1.BalloonTipText = $"Volume capped to {(int)(volumeCap * 100)}%";
                notifyIcon1.ShowBalloonTip(2000);
            }
        }

        // Required empty methods
        public void OnDeviceAdded(string pwstrDeviceId) { }
        public void OnDeviceRemoved(string deviceId) { }
        public void OnDeviceStateChanged(string deviceId, DeviceState newState) { }
        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key) { }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            isEnabled = checkBoxEnable.Checked;

            //  SAVE
            Properties.Settings.Default.IsEnabled = isEnabled;
            Properties.Settings.Default.Save();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            int value = trackBarVolume.Value;
            volumeCap = value / 100f;

            labelVolume.Text = $"Volume Cap: {value}%";

            //  SAVE
            Properties.Settings.Default.VolumeCap = volumeCap;
            Properties.Settings.Default.Save();
        }

        private void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            string appName = "SafeVolume";
            string exePath = Application.ExecutablePath;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(
                @"Software\Microsoft\Windows\CurrentVersion\Run", true);


            if (key == null) return;
            if (checkBoxStartup.Checked)
            {
                key.SetValue(appName, exePath);
            }
            else
            {
                key.DeleteValue(appName, false);
            }
            key.Close();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
    this.Show();
    this.WindowState = FormWindowState.Normal;
    this.ShowInTaskbar = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            allowExit = true;
            notifyIcon1.Visible = false;
            Application.Exit();
        }
    }
}