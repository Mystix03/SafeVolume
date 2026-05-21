using System;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace SafeVolume
{
    public partial class FormDevices : Form
    {
        private CheckedListBox checkedListBoxDevices;

        public FormDevices()
        {
            
            InitializeCustomUI();
            LoadStoredDevicesUI();
        }

        private void InitializeCustomUI()
        {
            // Configure the popup window properties
            this.Text = "Managed Devices";
            this.Size = new System.Drawing.Size(350, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // Dynamically create and place the checklist box
            checkedListBoxDevices = new CheckedListBox();
            checkedListBoxDevices.Dock = DockStyle.Fill;
            checkedListBoxDevices.CheckOnClick = true; // Checks the box on a single click
            checkedListBoxDevices.ItemCheck += CheckedListBoxDevices_ItemCheck;

            this.Controls.Add(checkedListBoxDevices);
        }

        private void LoadStoredDevicesUI()
        {
            checkedListBoxDevices.Items.Clear();

            if (Properties.Settings.Default.KnownDevices == null)
                Properties.Settings.Default.KnownDevices = new StringCollection();
            if (Properties.Settings.Default.ProtectedDevices == null)
                Properties.Settings.Default.ProtectedDevices = new StringCollection();

            var known = Properties.Settings.Default.KnownDevices;
            var protectedDevs = Properties.Settings.Default.ProtectedDevices;

            foreach (string deviceName in known)
            {
                bool isChecked = protectedDevs.Contains(deviceName);
                checkedListBoxDevices.Items.Add(deviceName, isChecked);
            }
        }

        private void CheckedListBoxDevices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // Create the updated collection
            StringCollection protectedDevs = new StringCollection();

            // Loop through currently checked items
            foreach (var item in checkedListBoxDevices.CheckedItems)
            {
                protectedDevs.Add(item.ToString());
            }

            // Account for the item currently being clicked right now
            string currentItem = checkedListBoxDevices.Items[e.Index].ToString();
            if (e.NewValue == CheckState.Checked)
            {
                if (!protectedDevs.Contains(currentItem))
                    protectedDevs.Add(currentItem);
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                if (protectedDevs.Contains(currentItem))
                    protectedDevs.Remove(currentItem);
            }

            // Save directly without Invoke
            Properties.Settings.Default.ProtectedDevices = protectedDevs;
            Properties.Settings.Default.Save();
        }
    }
}