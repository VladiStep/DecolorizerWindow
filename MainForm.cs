using DecolorizerWindow.Properties;
using static DecolorizerWindow.Core;

namespace DecolorizerWindow
{
    public partial class MainForm : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT;
                return cp;
            }
        }

        private readonly GlobalKeyboardHook globalKeyboardHook;

        public MainForm()
        {
            InitializeComponent();

            Core.MainForm = this;

            if (Core.Width == 0)
            {
                Core.Width = (ushort)(Screen.PrimaryScreen.Bounds.Width * 0.3);
                Core.Height = (ushort)(Screen.PrimaryScreen.Bounds.Height * 0.3);
                Settings.Default.Save();
            }

            globalKeyboardHook = new(new[] { Keys.Z });
            globalKeyboardHook.KeyboardPressed += OnKeyboardPressed;

            MakeFormInvisible(this);
            StartUpdating();
        }

        private async void OnKeyboardPressed(object sender, GlobalKeyboardHookEventArgs e)
        {
            if (e.KeyboardState == GlobalKeyboardHook.KeyboardState.SysKeyDown)
            {
                if (e.KeyboardData.Key == Keys.Z && (ModifierKeys & Keys.Alt) != 0)
                    await ToggleProgramState();
            }
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OpenSettings();
        }

        private void toggleDisplayMenuItem_Click(object sender, EventArgs e)
        {
            _ = ToggleProgramState();
        }
        private void settingsMenuItem_Click(object sender, EventArgs e)
        {
            OpenSettings();
        }
        private async void exitMenuItem_Click(object sender, EventArgs e)
        {
            await StopUpdating(showError: false);
            Application.Exit();
        }

        private void OpenSettings()
        {
            foreach (var form in Application.OpenForms)
            {
                if (form is SettingsForm setForm)
                {
                    setForm.WindowState = FormWindowState.Normal;
                    setForm.BringToFront();
                    setForm.Focus();

                    return;
                }
            }

            SettingsForm settingsForm = new();
            settingsForm.Show();
        }
    }
}
