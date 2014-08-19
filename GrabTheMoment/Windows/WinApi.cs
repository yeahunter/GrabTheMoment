using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Windows
{
    public class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void
        SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter,
            int X, int Y, int width, int height, uint flags);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0x0040

        public static int ScreenX
        {
            get { return GetSystemMetrics(SM_CXSCREEN); }
        }

        public static int ScreenY
        {
            get { return GetSystemMetrics(SM_CYSCREEN); }
        }

        public static void SetWinFullScreen(IntPtr hwnd, int x, int y)
        {
            SetWindowPos(hwnd, HWND_TOP, x, y, SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height, SWP_SHOWWINDOW);
        }
    }
}