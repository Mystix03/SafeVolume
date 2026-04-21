namespace SafeVolume
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            checkBoxEnable = new CheckBox();
            labelVolume = new Label();
            trackBarVolume = new TrackBar();
            checkBoxStartup = new CheckBox();
            notifyIcon1 = new NotifyIcon(components);
            contextMenuStrip1 = new ContextMenuStrip(components);
            showToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).BeginInit();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // checkBoxEnable
            // 
            checkBoxEnable.AutoSize = true;
            checkBoxEnable.Location = new Point(133, 52);
            checkBoxEnable.Name = "checkBoxEnable";
            checkBoxEnable.Size = new Size(176, 29);
            checkBoxEnable.TabIndex = 0;
            checkBoxEnable.Text = "Enable Protection";
            checkBoxEnable.UseVisualStyleBackColor = true;
            checkBoxEnable.CheckedChanged += checkBoxEnable_CheckedChanged;
            // 
            // labelVolume
            // 
            labelVolume.AutoSize = true;
            labelVolume.Location = new Point(133, 210);
            labelVolume.Name = "labelVolume";
            labelVolume.Size = new Size(152, 25);
            labelVolume.TabIndex = 1;
            labelVolume.Text = "Volume Cap: 30%";
            // 
            // trackBarVolume
            // 
            trackBarVolume.Location = new Point(133, 122);
            trackBarVolume.Maximum = 100;
            trackBarVolume.Name = "trackBarVolume";
            trackBarVolume.Size = new Size(156, 69);
            trackBarVolume.TabIndex = 2;
            trackBarVolume.Value = 30;
            trackBarVolume.Scroll += trackBarVolume_Scroll;
            // 
            // checkBoxStartup
            // 
            checkBoxStartup.AutoSize = true;
            checkBoxStartup.Location = new Point(197, 266);
            checkBoxStartup.Name = "checkBoxStartup";
            checkBoxStartup.Size = new Size(191, 29);
            checkBoxStartup.TabIndex = 3;
            checkBoxStartup.Text = "Start with Windows";
            checkBoxStartup.UseVisualStyleBackColor = true;
            checkBoxStartup.CheckedChanged += checkBoxStartup_CheckedChanged;
            // 
            // notifyIcon1
            // 
            notifyIcon1.ContextMenuStrip = contextMenuStrip1;
            notifyIcon1.Icon = (Icon)resources.GetObject("notifyIcon1.Icon");
            notifyIcon1.Text = "SafeVolume running";
            notifyIcon1.Visible = true;
            notifyIcon1.MouseDoubleClick += notifyIcon1_MouseDoubleClick;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.ImageScalingSize = new Size(24, 24);
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { showToolStripMenuItem, exitToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(129, 68);
            // 
            // showToolStripMenuItem
            // 
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(128, 32);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += showToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(128, 32);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(400, 296);
            Controls.Add(checkBoxStartup);
            Controls.Add(trackBarVolume);
            Controls.Add(labelVolume);
            Controls.Add(checkBoxEnable);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)trackBarVolume).EndInit();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox checkBoxEnable;
        private Label labelVolume;
        private TrackBar trackBarVolume;
        private CheckBox checkBoxStartup;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
    }
}
