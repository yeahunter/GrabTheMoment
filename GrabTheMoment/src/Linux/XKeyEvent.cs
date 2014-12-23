#if __MonoCS__
using System;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    [StructLayout(LayoutKind.Sequential)]
    public struct XKeyEvent
    {
        public XEventName type;
        public IntPtr serial;
        public bool send_event;
        public IntPtr display;
        public IntPtr window;
        public IntPtr root;
        public IntPtr subwindow;
        public IntPtr time;
        public int x;
        public int y;
        public int x_root;
        public int x_y;
        public uint state;
        public uint keycode;
        public bool same_screen;
    }
}
#endif