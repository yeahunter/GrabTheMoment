#if __MonoCS__
using System;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public class NativeLinux
    {
        [DllImport("libX11", EntryPoint = "XMoveResizeWindow")]
        public static extern int XMoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height);

        [DllImport("libX11")]
        public static extern int XKeysymToKeycode(IntPtr display, SpecialKey keysym);

        [DllImport("libX11")]
        public static extern void XGrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, 
            IntPtr window, bool owner_events, XGrabMode pointer_mode, XGrabMode keyboard_mode);

        [DllImport("libX11")]
        public static extern void XUngrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, IntPtr window);

        [DllImport("gdk-x11-2.0")]
        public static extern IntPtr gdk_x11_drawable_get_xid(IntPtr window);

        [DllImport("gdk-x11-2.0")]
        public static extern IntPtr gdk_x11_get_default_xdisplay();

        [DllImport("gdk-x11-2.0")]
        public static extern void gdk_error_trap_push();

        [DllImport("gdk-x11-2.0")]
        public static extern int gdk_error_trap_pop();

        [DllImport("gdk-x11-2.0")]
        public static extern void gdk_flush();
    }
}
#endif