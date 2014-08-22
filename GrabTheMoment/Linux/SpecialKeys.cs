#if __MonoCS__
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GrabTheMoment.Linux
{
    public class SpecialKeys
    {
        private Hashtable key_map = new Hashtable();
        private Hashtable key_registrations = new Hashtable();
        private Dictionary<int, Gdk.ModifierType> keycode_list;
        private TimeSpan raise_delay = new TimeSpan(0);
        private DateTime last_raise = DateTime.MinValue;

        public SpecialKeys()
        {
            Log.WriteEvent("init");
            keycode_list = BuildKeyCodeList();
            InitializeKeys();
        }

        public void Dispose()
        {
            UnitializeKeys();
        }

        public void RegisterHandler(SpecialKeyPressedHandler handler, params SpecialKey[] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys)
            {
                if(key_map.Contains(specialKey))
                {
                    int key = (int)key_map[specialKey];
                    key_registrations[key] = Delegate.Combine(key_registrations[key] as Delegate, handler);
                }
            }
        }

        public void UnregisterHandler(SpecialKeyPressedHandler handler, params SpecialKey[] specialKeys)
        {
            foreach(SpecialKey specialKey in specialKeys)
            {
                if(key_map.Contains(specialKey))
                {
                    int key = (int)key_map[specialKey];
                    key_registrations[key] = Delegate.Remove(key_registrations[key] as Delegate, handler); 
                }
            }
        }

        private Dictionary<int, Gdk.ModifierType> BuildKeyCodeList()
        {
            var kc_list = new Dictionary<int, Gdk.ModifierType>();

            foreach(SpecialKey key in Enum.GetValues(typeof(SpecialKey)))
            {
                IntPtr xdisplay = gdk_x11_get_default_xdisplay();

                if(!xdisplay.Equals(IntPtr.Zero))
                {
                    int keycode = XKeysymToKeycode(xdisplay, key);
                    if(keycode != 0)
                    {
                        key_map[keycode] = key;
                        key_map[key] = keycode;

                        if(key == SpecialKey.AltPrint)
                            kc_list.Add(keycode, Gdk.ModifierType.Mod1Mask);
                        else
                            kc_list.Add(keycode, Gdk.ModifierType.None);
                    }
                }
            }

            return kc_list;
        }

        private void InitializeKeys()
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++)
            {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);

                foreach(var klist in keycode_list)
                    GrabKey(screen.RootWindow, klist.Key, klist.Value);

                screen.RootWindow.AddFilter(FilterKey);
            }
        }

        private void UnitializeKeys() 
        {
            for(int i = 0; i < Gdk.Display.Default.NScreens; i++)
            {
                Gdk.Screen screen = Gdk.Display.Default.GetScreen(i);
                foreach(var klist in keycode_list)
                    UngrabKey(screen.RootWindow, klist.Key, klist.Value);

                screen.RootWindow.RemoveFilter(FilterKey);
            }
        }

        private void GrabKey(Gdk.Window root, int keycode, Gdk.ModifierType modemask = Gdk.ModifierType.None)
        {
            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            if(modemask != Gdk.ModifierType.None)
            {
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, modemask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, modemask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            }
            else
            {
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod5Mask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.Mod5Mask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod5Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
                XGrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.Mod5Mask | Gdk.ModifierType.LockMask, xid, true, XGrabMode.Async, XGrabMode.Async);
            }


            gdk_flush();

            if(gdk_error_trap_pop() != 0)
                Log.WriteEvent(string.Format(": Could not grab key {0} (maybe another application has grabbed this key)", keycode));
        }

        private void UngrabKey(Gdk.Window root, int keycode, Gdk.ModifierType modemask = Gdk.ModifierType.None)
        {
            IntPtr xid = gdk_x11_drawable_get_xid(root.Handle);
            IntPtr xdisplay = gdk_x11_get_default_xdisplay();

            gdk_error_trap_push();

            if(modemask != Gdk.ModifierType.None)
            {
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid);
                XUngrabKey(xdisplay, keycode, modemask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, modemask | Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | modemask | Gdk.ModifierType.LockMask, xid);
            }
            else
            {
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.None, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod5Mask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.Mod5Mask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod5Mask | Gdk.ModifierType.LockMask, xid);
                XUngrabKey(xdisplay, keycode, Gdk.ModifierType.Mod2Mask | Gdk.ModifierType.Mod5Mask | Gdk.ModifierType.LockMask, xid);
            }

            gdk_flush();

            if(gdk_error_trap_pop() != 0)
                Log.WriteEvent(string.Format(": Could not ungrab key {0} (maybe another application has grabbed this key)", keycode));
        }

        private Gdk.FilterReturn FilterKey(IntPtr xeventPtr, Gdk.Event gdkEvent)
        {
            Log.WriteEvent("filter "+ gdkEvent.Type);
            if(DateTime.Now - last_raise < raise_delay)
                return Gdk.FilterReturn.Continue;

            last_raise = DateTime.Now;

            XKeyEvent xevent = (XKeyEvent)Marshal.PtrToStructure(xeventPtr, typeof(XKeyEvent));

            if(xevent.type != XEventName.KeyPress)
                return Gdk.FilterReturn.Continue;

            int keycode = (int)xevent.keycode;
            object x = key_map[keycode];

            Log.WriteEvent("filter "+ keycode);

            if(x == null)
                return Gdk.FilterReturn.Continue;

            SpecialKey key = (SpecialKey)key_map[keycode];

            if (key_registrations[keycode] != null)
            {
                x = key_registrations[keycode];
                if (x is SpecialKeyPressedHandler)
                    ((SpecialKeyPressedHandler)x)(this, key);    

                return Gdk.FilterReturn.Remove;
            }

            return Gdk.FilterReturn.Continue;
        }

        public TimeSpan Delay
        {
            get { return raise_delay; }
            set { raise_delay = value; }
        }

        [DllImport("libX11")]
        private static extern int XKeysymToKeycode(IntPtr display, SpecialKey keysym);

        [DllImport("libX11")]
        private static extern void XGrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, 
            IntPtr window, bool owner_events, XGrabMode pointer_mode, XGrabMode keyboard_mode);

        [DllImport("libX11")]
        private static extern void XUngrabKey(IntPtr display, int keycode, Gdk.ModifierType modifiers, 
            IntPtr window);

        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_drawable_get_xid(IntPtr window);

        [DllImport("gdk-x11-2.0")]
        private static extern IntPtr gdk_x11_get_default_xdisplay();

        [DllImport("gdk-x11-2.0")]
        private static extern void gdk_error_trap_push();

        [DllImport("gdk-x11-2.0")]
        private static extern int gdk_error_trap_pop();

        [DllImport("gdk-x11-2.0")]
        private static extern void gdk_flush();     
    }
}
#endif