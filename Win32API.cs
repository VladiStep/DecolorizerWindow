using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DecolorizerWindow
{
    public static unsafe partial class Core
    {
        public const int WS_EX_TRANSPARENT = 0x00000020;
        private const int WS_EX_NOACTIVATE = 0x08000000;
        private const int HWND_TOPMOST = -1;
        private const uint SWP_NOSIZE = 0x1;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint DIB_RGB_COLORS = 0x0;
        private const uint SRCCOPY = 0xCC0020;
        private const uint CAPTUREBLT = 0x40000000;
        private const uint WDA_EXCLUDEFROMCAPTURE = 0x00000011;

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        public struct RGBQUAD
        {
            public byte rgbBlue;
            public byte rgbGreen;
            public byte rgbRed;
            public byte rgbReserved;
        }

        public enum BI
        {
            RGB = 0,
            RLE8 = 1,
            RLE4 = 2,
            BITFIELDS = 3,
            JPEG = 4,
            PNG = 5,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFOHEADER
        {
            public uint biSize;
            public int biWidth;
            public int biHeight;
            public ushort biPlanes;
            public ushort biBitCount;
            public BI biCompression;
            public uint biSizeImage;
            public int biXPelsPerMeter;
            public int biYPelsPerMeter;
            public uint biClrUsed;
            public uint biClrImportant;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public BITMAPINFOHEADER bmiHeader;
            public RGBQUAD bmiColors;
        }
        #endregion


        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(ref Point lpPoint);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetWindowDisplayAffinity([In] IntPtr hwnd, uint dwAffinity);

        [DllImport("user32.dll")]
        static extern bool SetWindowPos([In] IntPtr hWnd, [In] IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);


        [DllImport("user32.dll")]
        private static extern IntPtr GetDC([In] IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern int ReleaseDC([In] IntPtr hWnd, [In] IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC([In] IntPtr hDC);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject([In] IntPtr hObject);

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteDC([In] IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject([In] IntPtr hDC, [In] IntPtr hObject);

        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateDIBSection([In] IntPtr hdc, ref BITMAPINFO pbmi, uint iUsage,
                                                      out IntPtr ppvBits, [In] IntPtr hSection, uint dwOffset);
//                                                        byte*

        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool BitBlt([In] IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight,
                                          [In] IntPtr hObjectSource, int nXSrc, int nYSrc, uint dwRop);
    }
}
