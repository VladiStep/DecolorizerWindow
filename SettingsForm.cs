using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DecolorizerWindow.Core;
using DecolorizerWindow.Properties;

namespace DecolorizerWindow
{
    public partial class SettingsForm : Form
    {
        private readonly ushort screenWidth = (ushort)Screen.PrimaryScreen.Bounds.Width;
        private readonly ushort screenHeight = (ushort)Screen.PrimaryScreen.Bounds.Height;

        public SettingsForm()
        {
            InitializeComponent();

            widthTextBox.Text = Settings.Default.Width.ToString();
            heightTextBox.Text = Settings.Default.Height.ToString();
        }

        private void widthTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!UInt16.TryParse(widthTextBox.Text, out ushort width)
                || width < 1 || width > screenWidth)
            {
                e.Cancel = true;
                widthTextBox.SelectAll();

                errorProvider.SetError(widthTextBox, $"Укажите положительное число от 1 до {screenWidth}.");
            }
        }
        private void heightTextBox_Validating(object sender, CancelEventArgs e)
        {
            if (!UInt16.TryParse(heightTextBox.Text, out ushort height)
                || height < 1 || height > screenHeight)
            {
                e.Cancel = true;
                heightTextBox.SelectAll();

                errorProvider.SetError(heightTextBox, $"Укажите положительное число от 1 до {screenHeight}.");
            }
        }
        private void textBox_Validated(object sender, EventArgs e)
        {
            if (sender is not TextBox textBox)
                return;

            errorProvider.SetError(textBox, "");
        }

        private async void saveButton_Click(object sender, EventArgs e)
        {
            bool sizeChanged = false;

            if (UInt16.TryParse(widthTextBox.Text, out ushort width))
            {
                if (width != Settings.Default.Width)
                    sizeChanged = true;

                Settings.Default.Width = width;
            }
            if (UInt16.TryParse(heightTextBox.Text, out ushort height))
            {
                if (height != Settings.Default.Height)
                    sizeChanged = true;

                Settings.Default.Height = height;
            }

            Settings.Default.Save();

            if (sizeChanged)
            {
                bool stopped = await StopUpdating();
                if (stopped)
                    StartUpdating();
            }
        }
    }
}
