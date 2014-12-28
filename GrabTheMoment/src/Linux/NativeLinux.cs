#if __MonoCS__
using System;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public class NativeLinux
    {
        [DllImport("libX11", EntryPoint = "XMoveResizeWindow")]
        public static extern int XMoveResizeWindow(IntPtr display, IntPtr window, int x, int y, int width, int height);

        [DllImport ("libX11", EntryPoint = "XFlush")]
        public static extern int XFlush(IntPtr display);

        [DllImport("libX11")]
        public static extern int XKeysymToKeycode(IntPtr display, SpecialKey keysym);

        [DllImport("libX11")]
        public static extern void XGrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, 
            IntPtr window, bool owner_events, XGrabMode pointer_mode, XGrabMode keyboard_mode);

        [DllImport("libX11")]
        public static extern void XUngrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, IntPtr window);

        [DllImport("libX11", EntryPoint = "XFetchName")]
        public static extern int XFetchName(IntPtr display, IntPtr window, ref IntPtr window_name);

        [DllImport("libX11", EntryPoint = "XFree")]
        public static extern int XFree(IntPtr data);

        [DllImport("libgdk-x11-2.0.so.0")]
        public static extern IntPtr gdk_x11_drawable_get_xid(IntPtr window);

        [DllImport("libgdk-x11-2.0.so.0")]
        public static extern IntPtr gdk_x11_get_default_xdisplay();

        [DllImport("libgdk-x11-2.0.so.0")]
        public static extern void gdk_error_trap_push();

        [DllImport("libgdk-x11-2.0.so.0")]
        public static extern int gdk_error_trap_pop();

        [DllImport("libgdk-x11-2.0.so.0")]
        public static extern void gdk_flush();

        /// <summary>
        /// Gets the text of the window.
        /// </summary>
        /// <param name="windowPointer">The pointer to the window.</param>
        /// <returns>The text of the window.</returns>
        public static string GetWindowText(Gdk.Window windowPointer)
        {
            string windowText = string.Empty;
            IntPtr xid = gdk_x11_drawable_get_xid(windowPointer.Handle);
            IntPtr display = gdk_x11_get_default_xdisplay();
            IntPtr namePointer = IntPtr.Zero;
            int success = XFetchName(display, xid, ref namePointer);
            string name = Marshal.PtrToStringAuto(namePointer);

            if (success != 0)
                windowText = name;

            XFree(namePointer);
            return windowText;
        }
    }
}
#endif