namespace DecolorizerWindow
{
    partial class MainForm
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
            displayPicture = new PictureBox();
            trayIcon = new NotifyIcon(components);
            trayContextMenu = new ContextMenuStrip(components);
            settingsMenuItem = new ToolStripMenuItem();
            toggleDisplayMenuItem = new ToolStripMenuItem();
            exitMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)displayPicture).BeginInit();
            trayContextMenu.SuspendLayout();
            SuspendLayout();
            // 
            // displayPicture
            // 
            displayPicture.BackColor = SystemColors.Control;
            displayPicture.Location = new Point(0, 0);
            displayPicture.Margin = new Padding(0);
            displayPicture.Name = "displayPicture";
            displayPicture.Size = new Size(572, 320);
            displayPicture.TabIndex = 0;
            displayPicture.TabStop = false;
            // 
            // trayIcon
            // 
            trayIcon.BalloonTipTitle = "Обесцветыватель";
            trayIcon.ContextMenuStrip = trayContextMenu;
            trayIcon.Icon = Properties.Resources.mainIcon;
            trayIcon.Text = "Обесцветыватель";
            trayIcon.Visible = true;
            trayIcon.MouseClick += trayIcon_MouseClick;
            // 
            // trayContextMenu
            // 
            trayContextMenu.Items.AddRange(new ToolStripItem[] { settingsMenuItem, toggleDisplayMenuItem, exitMenuItem });
            trayContextMenu.Name = "trayContextMenu";
            trayContextMenu.Size = new Size(317, 92);
            // 
            // settingsMenuItem
            // 
            settingsMenuItem.Image = Properties.Resources.settingsIcon;
            settingsMenuItem.Name = "settingsMenuItem";
            settingsMenuItem.Size = new Size(275, 22);
            settingsMenuItem.Text = "Открыть окно настроек";
            settingsMenuItem.Click += settingsMenuItem_Click;
            // 
            // toggleDisplayMenuItem
            // 
            toggleDisplayMenuItem.Image = Properties.Resources.toggleDisplayIcon;
            toggleDisplayMenuItem.Name = "toggleDisplayMenuItem";
            toggleDisplayMenuItem.Size = new Size(316, 22);
            toggleDisplayMenuItem.Text = "Переключить отображение области (Alt+Z)";
            toggleDisplayMenuItem.Click += toggleDisplayMenuItem_Click;
            // 
            // exitMenuItem
            // 
            exitMenuItem.Image = Properties.Resources.exitIcon;
            exitMenuItem.Name = "exitMenuItem";
            exitMenuItem.Size = new Size(275, 22);
            exitMenuItem.Text = "Закрыть приложение (выход)";
            exitMenuItem.Click += exitMenuItem_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(572, 320);
            ControlBox = false;
            Controls.Add(displayPicture);
            FormBorderStyle = FormBorderStyle.None;
            Icon = Properties.Resources.mainIcon;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            Opacity = 0D;
            ShowIcon = false;
            ShowInTaskbar = false;
            SizeGripStyle = SizeGripStyle.Hide;
            Tag = "JustCreated";
            Text = "Обесцветыватель";
            TopMost = true;
            TransparencyKey = SystemColors.Control;
            ((System.ComponentModel.ISupportInitialize)displayPicture).EndInit();
            trayContextMenu.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public PictureBox displayPicture;
        public NotifyIcon trayIcon;
        private ContextMenuStrip trayContextMenu;
        private ToolStripMenuItem settingsMenuItem;
        private ToolStripMenuItem toggleDisplayMenuItem;
        private ToolStripMenuItem exitMenuItem;
    }
}
