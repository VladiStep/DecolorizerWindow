using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecolorizerWindow
{
    public static partial class Core
    {
        public static MainForm MainForm { get; set; }

        public static ushort Width
        {
            get => Properties.Settings.Default.Width;
            set => Properties.Settings.Default.Width = value;
        }
        public static ushort Height
        {
            get => Properties.Settings.Default.Height;
            set => Properties.Settings.Default.Height = value;
        }

        private static bool updating = true;
        private static bool stopping = false;
        private static bool finished = false;
        private static bool toggleCooledDown = true;
        private static readonly ushort bytesPerPixel = (ushort)(Screen.PrimaryScreen.BitsPerPixel / 8);
        private static readonly byte lastByteSkip = (byte)(bytesPerPixel == 3 ? 1 : 2);

        public static void ShowError(string message)
        {
            MessageBox.Show(message, "Обесцветыватель", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void MakeFormInvisible(Form form)
        {
            // Сделать окно невидимым для "BitBlt()" (функции копирования пикселей)
            SetWindowDisplayAffinity(form.Handle, WDA_EXCLUDEFROMCAPTURE);
        }

        public static async void StartUpdating()
        {
            await Task.Delay(1); // Чтобы компилятор думал, что этот метод по-настоящему асинхронен

            finished = false;
            updating = true;

            Size size = new(Width, Height);
            MainForm.Size = size;
            MainForm.displayPicture.Size = size;
            using Graphics g = MainForm.displayPicture.CreateGraphics();

            IntPtr hScreen = GetDC((IntPtr)null);
            IntPtr srcHDC = CreateCompatibleDC(hScreen);
            IntPtr destHDC = g.GetHdc();

            BITMAPINFO bmi = default;
            unsafe
            {
                bmi.bmiHeader.biSize = (uint)sizeof(BITMAPINFOHEADER);
            }
            bmi.bmiHeader.biWidth = Width;
            bmi.bmiHeader.biHeight = -Height; // top-down
            bmi.bmiHeader.biPlanes = 1;
            bmi.bmiHeader.biBitCount = (ushort)Screen.PrimaryScreen.BitsPerPixel;
            bmi.bmiHeader.biCompression = BI.RGB;
            int bufSize = ((((Width * bmi.bmiHeader.biBitCount) + 31) & ~31) >> 3) * Height;

            IntPtr pBits;
            IntPtr hBitmap = CreateDIBSection(srcHDC, ref bmi, DIB_RGB_COLORS, out pBits, (IntPtr)null, 0);
            if (hBitmap == IntPtr.Zero)
            {
                ShowError("Ошибка при вызове GDI32.CreateDIBSection().");
                _ = ReleaseDC((IntPtr)null, hScreen);
                DeleteDC(srcHDC);
                DeleteDC(destHDC);
                
                return;
            }

            int halfWidth = Width / 2;
            int halfHeight = Height / 2;
            uint windowFlag = SWP_NOSIZE | SWP_NOACTIVATE;

            SelectObject(srcHDC, hBitmap);

            // Нельзя сделать "Visible = false" при первом запуске, поэтому я
            // проверяю "Tag" - если он не пуст, значит это первый запуск
            if (!MainForm.Visible || MainForm.Tag is not null)
            {
                MainForm.Tag = null;

                // Показать окно когда уже начнётся сам цикл копирования
                _ = Task.Run(() =>
                {
                    Thread.Sleep(30);
                    MainForm.Invoke(() =>
                    {
                        // Чтобы не было белого кадра при запуске, приходится также
                        // менять прозрачность окна, т.к. почему-то при выставлении размера окна
                        // оно становится видимым не смотря на "TransparencyKey"
                        MainForm.Opacity = 1;
                        MainForm.Show();
                    });
                });
            }

            while (updating)
            {
                Point mousePos = default;
                GetCursorPos(ref mousePos);
                int x = mousePos.X - halfWidth;
                int y = mousePos.Y - halfHeight;

                BitBlt(srcHDC, 0, 0, Width, Height, hScreen, x, y, SRCCOPY); // Копировать пиксели с экрана
                TransformAndCopy(pBits, bufSize, srcHDC, destHDC, hBitmap); // Сделать чёрно-белым и скопировать пиксели в окно

                SetWindowPos(MainForm.Handle, HWND_TOPMOST, x, y, 0, 0, windowFlag);

                Application.DoEvents(); // Предотвратить "Приложение не отвечает"
            }

            _ = ReleaseDC((IntPtr)null, hScreen);
            DeleteDC(srcHDC);
            DeleteDC(destHDC);
            g.ReleaseHdc();
            DeleteObject(hBitmap);

            finished = true;
        }
        private static unsafe void TransformAndCopy(IntPtr srcBufPtr, int bufSize, IntPtr srcHDC, IntPtr destHDC, IntPtr hBitmap)
        {
            byte* srcBuf = (byte*)srcBufPtr.ToPointer();
            for (int i = 0; i < bufSize; i += bytesPerPixel)
            {
                // "srcBuf++" - вернуть значение, а потом увеличить его на 1.
                byte* b = srcBuf++;
                byte* g = srcBuf++;
                byte* r = srcBuf;
                srcBuf += lastByteSkip;

                byte gray = (byte)(*r * 0.3 + *g * 0.59 + *b * 0.11);
                *b = *g = *r = gray;
            }

            BitBlt(destHDC, 0, 0, Width, Height, srcHDC, 0, 0, SRCCOPY);
        }

        public static async Task<bool> StopUpdating(bool showError = true)
        {
            if (!updating)
                return true;

            updating = false;

            bool res = await Task.Run(() =>
            {
                if (finished)
                    return true;

                for (int i = 0; i < 50; i++) // ждать 5 секунд
                {
                    Thread.Sleep(100);
                    if (finished)
                        return true;
                }

                return false;
            });

            if (!res)
            {
                if (showError)
                    ShowError("Не получилось остановить процесс обновления изображения.\n" +
                              "Приложение будет принудительно остановлено.");

                Environment.Exit(1);
            }

            return true;
        }

        public static async Task ToggleProgramState()
        {
            if (!toggleCooledDown)
                return;

            toggleCooledDown = false;
            _ = Task.Run(() =>
            {
                Thread.Sleep(200); // Не чаще, чем 5 раз в секунду 
                toggleCooledDown = true;
            });

            if (updating)
            {
                if (!stopping)
                {
                    stopping = true;

                    MainForm.Hide();
                    MainForm.Opacity = 0;
                    await StopUpdating();

                    stopping = false;
                }
            }
            else
            {
                StartUpdating();
            }
        }
    }
}
