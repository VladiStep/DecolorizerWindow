using System.Diagnostics;

namespace DecolorizerWindow
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Process[] instances = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (instances.Length > 1)
            {
                MessageBox.Show("Программа уже открыта, попробуйте показать окно через трей или нажав Alt+Z.",
                                "Обесцветыватель", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}