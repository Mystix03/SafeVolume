using System;
using System.Windows.Forms;
using System.Collections.Specialized;
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

        // Anti-spam mechanics
        private System.Windows.Forms.Timer notificationTimer;
        private string pendingNotificationDevice = "";
        private readonly object notificationLock = new object();

        public Form1()
        {
            InitializeComponent();

            volumeCap = Properties.Settings.Default.VolumeCap;
            isEnabled = Properties.Settings.Default.IsEnabled;

            trackBarVolume.Value = (int)(volumeCap * 100);
            checkBoxEnable.Checked = isEnabled;
            labelVolume.Text = $"Volume Cap: {trackBarVolume.Value}%";

            string appName = "SafeVolume";
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false))
            {
                if (key != null && key.GetValue(appName) != null)
                {
                    checkBoxStartup.Checked = true;
                }
            }

            // Set up the debounce timer (waits 1 second for audio signals to settle)
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 1000;
            notificationTimer.Tick += NotificationTimer_Tick;

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
                e.Cancel = true;
                this.Hide();
            }
        }

        private void btnDevices_Click(object sender, EventArgs e)
        {
            using (FormDevices devicesWindow = new FormDevices())
            {
                devicesWindow.ShowDialog(this);
            }
        }

        public void OnDefaultDeviceChanged(DataFlow flow, Role role, string defaultDeviceId)
        {
            if (flow == DataFlow.Render)
            {
                var device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
                string currentDeviceName = device.FriendlyName;

                System.Console.WriteLine($"Device Connected: {device.ID} | Name: {currentDeviceName}");

                if (Properties.Settings.Default.KnownDevices == null)
                    Properties.Settings.Default.KnownDevices = new StringCollection();

                if (!Properties.Settings.Default.KnownDevices.Contains(currentDeviceName))
                {
                    Properties.Settings.Default.KnownDevices.Add(currentDeviceName);
                    Properties.Settings.Default.Save();
                }

                if (isEnabled)
                {
                    var protectedDevs = Properties.Settings.Default.ProtectedDevices;

                    if (protectedDevs != null && protectedDevs.Contains(currentDeviceName))
                    {
                        // Instantly force the volume cap down silently
                        device.AudioEndpointVolume.MasterVolumeLevelScalar = volumeCap;

                        // Thread-safe notification staging
                        lock (notificationLock)
                        {
                            pendingNotificationDevice = currentDeviceName;

                            if (this.IsHandleCreated && !this.IsDisposed)
                            {
                                this.BeginInvoke((MethodInvoker)delegate
                                {
                                    // Restart the timer every time Windows spam-fires an event.
                                    // This delays the popup until the absolute LAST event passes.
                                    notificationTimer.Stop();
                                    notificationTimer.Start();
                                });
                            }
                        }
                    }
                }
            }
        }

        // Fires exactly ONCE, 1 second after Windows finishes bouncing connection events
        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            notificationTimer.Stop(); // Stand down until next device swap

            if (!string.IsNullOrEmpty(pendingNotificationDevice))
            {
                notifyIcon1.BalloonTipTitle = "SafeVolume Active";
                notifyIcon1.BalloonTipText = $"Capped {pendingNotificationDevice} to {(int)(volumeCap * 100)}%";
                notifyIcon1.ShowBalloonTip(2000);

                pendingNotificationDevice = ""; // Reset
            }
        }

        public void OnDeviceAdded(string pwstrDeviceId) { }
        public void OnDeviceRemoved(string deviceId) { }
        public void OnDeviceStateChanged(string deviceId, DeviceState newState) { }
        public void OnPropertyValueChanged(string pwstrDeviceId, PropertyKey key) { }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            isEnabled = checkBoxEnable.Checked;
            Properties.Settings.Default.IsEnabled = isEnabled;
            Properties.Settings.Default.Save();
        }

        private void trackBarVolume_Scroll(object sender, EventArgs e)
        {
            int value = trackBarVolume.Value;
            volumeCap = value / 100f;
            labelVolume.Text = $"Volume Cap: {value}%";

            Properties.Settings.Default.VolumeCap = volumeCap;
            Properties.Settings.Default.Save();
        }

        private void checkBoxStartup_CheckedChanged(object sender, EventArgs e)
        {
            string appName = "SafeVolume";
            string exePath = Application.ExecutablePath;

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key == null) return;
                if (checkBoxStartup.Checked)
                    key.SetValue(appName, exePath);
                else
                    key.DeleteValue(appName, false);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e) => ShowForm();
        private void showToolStripMenuItem_Click(object sender, EventArgs e) => ShowForm();

        private void ShowForm()
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